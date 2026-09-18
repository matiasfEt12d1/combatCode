using Persistencia.Entidades;

namespace Aplicacion.Interfaces;

public interface IPersonajeServicio
{
    Task<int> RegistrarPersonajeAsync(int usuarioId, string nombre, string tipo, double vidaMax, double fuerzaBase);
    Task<PersonajeTabla?> ObtenerPorIdAsync(int id);
    Task<IEnumerable<PersonajeTabla>> ObtenerPorUsuarioIdAsync(int usuarioId);
    Task<bool> ActualizarSaludAsync(int personajeId, double nuevaVida);
}