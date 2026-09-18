using Persistencia.Entidades;

namespace Aplicacion.Interfaces;

public interface IBatallaRepositorio
{
    Task<int> IniciarBatallaAsync(int atacanteId, int defensorId);
    Task CerrarBatallaTransaccionalAsync(int batallaId, int ganadorId, int perdedorId, double vidaRestanteGanador, int rondaFinal, double ultimoGolpe);
    Task<BatallaTabla?> ObtenerPorIdAsync(int id);
}