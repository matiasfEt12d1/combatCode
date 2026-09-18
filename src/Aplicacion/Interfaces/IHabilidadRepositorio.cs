using Persistencia.Entidades;

namespace Aplicacion.Interfaces;

public interface IHabilidadRepositorio
{
    Task<IEnumerable<HabilidadTabla>> ObtenerPorTipoPersonajeAsync(string tipoPersonaje);
    Task<HabilidadTabla?> ObtenerPorIdAsync(int id);
}