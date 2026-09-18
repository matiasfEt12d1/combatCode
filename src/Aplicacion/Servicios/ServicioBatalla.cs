using Aplicacion.Interfaces;
using Persistencia;

namespace Aplicacion.Servicios;

public class ServicioBatalla : IBatallaServicio
{
    private readonly IBatallaRepositorio _batallaRepositorio;
    private readonly IPersonajeRepositorio _personajeRepositorio;
    private readonly IHabilidadRepositorio _habilidadRepositorio;

    public ServicioBatalla(
        IBatallaRepositorio batallaRepositorio, 
        IPersonajeRepositorio personajeRepositorio,
        IHabilidadRepositorio habilidadRepositorio)
    {
        _batallaRepositorio = batallaRepositorio;
        _personajeRepositorio = personajeRepositorio;
        _habilidadRepositorio = habilidadRepositorio;
    }

    public async Task<int> CrearBatallaAsync(int atacanteId, int defensorId)
    {
        return await _batallaRepositorio.IniciarBatallaAsync(atacanteId, defensorId);
    }

    public async Task<(double Danio, bool EsCritico)> EjecutarTurnoPolimorficoAsync(int personajeAtacanteId, int personajeDefensorId, int? habilidadId = null)
    {
        var dtoAtacante = await _personajeRepositorio.ObtenerPorIdAsync(personajeAtacanteId)
            ?? throw new InvalidOperationException("Atacante no encontrado.");
        
        var dtoDefensor = await _personajeRepositorio.ObtenerPorIdAsync(personajeDefensorId)
            ?? throw new InvalidOperationException("Defensor no encontrado.");

        if (dtoAtacante.VidaActual <= 0)
            throw new InvalidOperationException("El personaje atacante está fuera de combate.");

        double danioBase = dtoAtacante.FuerzaBase;
        bool esCritico = false;

        if (habilidadId.HasValue)
        {
            var habilidad = await _habilidadRepositorio.ObtenerPorIdAsync(habilidadId.Value)
                ?? throw new InvalidOperationException("Habilidad no encontrada.");
            
            danioBase += habilidad.DanioBase;
        }

        if (dtoAtacante.TipoPersonaje.ToLower() == "asesino")
        {
            esCritico = new Random().Next(1, 100) <= 30;
            if (esCritico) danioBase *= 1.5;
        }

        double armadura = dtoDefensor.Armadura ?? 0;
        double danioFinal = Math.Max(1, danioBase - armadura);

        double nuevaVidaDefensor = Math.Max(0, dtoDefensor.VidaActual - danioFinal);
        await _personajeRepositorio.ActualizarVidaAsync(personajeDefensorId, nuevaVidaDefensor);

        return (danioFinal, esCritico);
    }

    public async Task<int> IniciarBatallaAsync(int personaje1Id, int personaje2Id)
    {
        var p1 = await _personajeRepositorio.ObtenerPorIdAsync(personaje1Id);
        var p2 = await _personajeRepositorio.ObtenerPorIdAsync(personaje2Id);

        if (p1 == null || p2 == null)
            throw new InvalidOperationException("Uno o ambos personajes no existen para iniciar la batalla.");

        return await _batallaRepositorio.IniciarBatallaAsync(personaje1Id, personaje2Id);
    }

    public async Task ProcesarFinalBatallaAsync(int batallaId, int ganadorId, int perdedorId, double vidaRestante, int totalRondas, double ultimoGolpe)
    {
        await _batallaRepositorio.CerrarBatallaTransaccionalAsync(
            batallaId,
            ganadorId,
            perdedorId,
            vidaRestante,
            totalRondas,
            ultimoGolpe
        );
    }
}