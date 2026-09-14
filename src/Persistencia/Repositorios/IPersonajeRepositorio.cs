using Persistencia.Entidades;

namespace Persistencia.Repositorios;

public interface IPersonajeRepositorio
{
    Task<PersonajeTabla?> ObtenerPorIdAsync(int id);
    Task<IEnumerable<PersonajeTabla>> ObtenerTodosAsync();
    Task<int> GuardarAsync(PersonajeTabla personaje);
    Task<bool> ActualizarVidaAsync(int personajeId, double nuevaVida);
}