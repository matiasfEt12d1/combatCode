namespace Aplicacion.Interfaces;

public interface IBatallaServicio
{
    Task<int> CrearBatallaAsync(int atacanteId, int defensorId);
    Task<(double Danio, bool EsCritico)> EjecutarTurnoPolimorficoAsync(int personajeAtacanteId, int personajeDefensorId, int? habilidadId = null);
    Task<int> IniciarBatallaAsync(int personaje1Id, int personaje2Id);
    Task ProcesarFinalBatallaAsync(int batallaId, int ganadorId, int perdedorId, double vidaRestante, int totalRondas, double ultimoGolpe);
}