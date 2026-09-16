using Persistencia.Entidades;

namespace Aplicacion.Interfaces;

public interface IPersonajeRepositorio
{
    Task<PersonajesTabla?> ObtenerPorIdAsync(int id);
    Task<IEnumerable<PersonajesTabla>> ObtenerPorUsuarioIdAsync(int usuarioId);
    Task<int> CrearAsync(PersonajesTabla personaje);
    Task<bool> ActualizarVidaAsync(int id, double nuevaVida);
}