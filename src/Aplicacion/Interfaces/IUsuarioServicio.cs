using Persistencia.Entidades;

namespace Aplicacion.Interfaces;

public interface IUsuarioServicio
{
    Task<bool> RegistrarUsuarioAsync(string nombreUsuario, string password);
    Task<UsuarioTabla?> AutenticarAsync(string nombreUsuario, string password);
}