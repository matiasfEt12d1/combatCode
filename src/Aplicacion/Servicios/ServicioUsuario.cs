using Aplicacion.Interfaces;
using Persistencia.Entidades;

namespace Aplicacion.Servicios;

public class ServicioUsuario : IUsuarioServicio
{
    private readonly IUsuarioRepositorio _usuarioRepositorio;

    public ServicioUsuario(IUsuarioRepositorio usuarioRepositorio)
    {
        _usuarioRepositorio = usuarioRepositorio;
    }

    public async Task<bool> RegistrarUsuarioAsync(string nombreUsuario, string password)
    {
        var existe = await _usuarioRepositorio.ObtenerPorNombreUsuarioAsync(nombreUsuario);
        if (existe != null) return false;

        // Hash de contraseña con BCrypt
        string hash = BCrypt.Net.BCrypt.HashPassword(password);
        
        var id = await _usuarioRepositorio.CrearUsuarioAsync(nombreUsuario, hash);
        return id > 0;
    }

    public async Task<UsuarioTabla?> AutenticarAsync(string nombreUsuario, string password)
    {
        var usuario = await _usuarioRepositorio.ObtenerPorNombreUsuarioAsync(nombreUsuario);
        if (usuario == null) return null;

        // Verificación del hash de contraseña
        bool valida = BCrypt.Net.BCrypt.Verify(password, usuario.PasswordHash);
        return valida ? usuario : null;
    }
}