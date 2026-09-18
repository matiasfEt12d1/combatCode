using Aplicacion.Interfaces;

namespace Aplicacion.Servicios;

public class ServicioBatalla : IBatallaServicio
{
    private readonly IBatallaRepositorio _batallaRepositorio;
    private readonly IPersonajeRepositorio _personajeRepositorio;

    public ServicioBatalla(IBatallaRepositorio batallaRepositorio, IPersonajeRepositorio personajeRepositorio)
    {
        _batallaRepositorio = batallaRepositorio;
        _personajeRepositorio = personajeRepositorio;
    }

    public async Task<int> CrearBatallaAsync(int atacanteId, int defensorId)
    {
        return await _batallaRepositorio.IniciarBatallaAsync(atacanteId, defensorId);
    }

    public async Task<int> IniciarBatallaAsync(int personaje1Id, int personaje2Id)
    {
        var p1 = await _personajeRepositorio.ObtenerPorIdAsync(personaje1Id);
        var p2 = await _personajeRepositorio.ObtenerPorIdAsync(personaje2Id);

        if (p1 == null || p2 == null)
            throw new InvalidOperationException("Uno o ambos personajes no existen para iniciar la batalla.");

        // Llama al Stored Procedure mediante el repositorio
        return await _batallaRepositorio.IniciarBatallaAsync(personaje1Id, personaje2Id);
    }

    public async Task ProcesarFinalBatallaAsync(int batallaId, int ganadorId, int perdedorId, double vidaRestante, int totalRondas, double ultimoGolpe)
    {
        // Ejecuta la transacción atómica de cierre mediante Stored Procedure
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