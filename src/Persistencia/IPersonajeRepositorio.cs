using Persistencia.Entidades;

namespace Aplicacion.Interfaces;

public interface IPersonajeRepositorio
{
    Task<PersonajeTabla?> ObtenerPorIdAsync(int id);
    Task<IEnumerable<PersonajeTabla>> ObtenerPorUsuarioIdAsync(int usuarioId);
    Task<int> CrearAsync(PersonajeTabla personaje);
    Task<bool> ActualizarVidaAsync(int id, double nuevaVida);
}