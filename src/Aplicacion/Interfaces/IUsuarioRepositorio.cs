using Persistencia.Entidades;

namespace Aplicacion.Interfaces;

public interface IUsuarioRepositorio
{
    Task<UsuarioTabla?> ObtenerPorIdAsync(int id);
    Task<UsuarioTabla?> ObtenerPorNombreUsuarioAsync(string nombreUsuario);
    Task<int> CrearUsuarioAsync(string nombreUsuario, string passwordHash);
}