namespace Aplicacion.Interfaces;

public interface IBatallaServicio
{
    Task<int> IniciarBatallaAsync(int personaje1Id, int personaje2Id);
    Task ProcesarFinalBatallaAsync(int batallaId, int ganadorId, int perdedorId, double vidaRestante, int totalRondas, double ultimoGolpe);
}