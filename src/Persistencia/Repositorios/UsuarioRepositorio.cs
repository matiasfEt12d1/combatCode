using System.Data;
using Aplicacion.Interfaces;
using Persistencia.Entidades;

namespace Persistencia.Repositorios;

public class UsuarioRepositorio : IUsuarioRepositorio
{
    private readonly IConexionDapper _dapper;

    public UsuarioRepositorio(IConexionDapper dapper)
    {
        _dapper = dapper;
    }

    public async Task<UsuarioTabla?> ObtenerPorIdAsync(int id)
    {
        var sql = "SELECT * FROM Usuarios WHERE Id = @Id;";
        return await _dapper.QueryFirstAsync<UsuarioTabla>(sql, new { Id = id }, CommandType.Text);
    }

    public async Task<UsuarioTabla?> ObtenerPorNombreUsuarioAsync(string nombreUsuario)
    {
        var sql = "SELECT * FROM Usuarios WHERE NombreUsuario = @NombreUsuario;";
        return await _dapper.QueryFirstAsync<UsuarioTabla>(sql, new { NombreUsuario = nombreUsuario }, CommandType.Text);
    }

    public async Task<int> CrearUsuarioAsync(string nombreUsuario, string passwordHash)
    {
        var sql = @"INSERT INTO Usuarios (NombreUsuario, PasswordHash) 
                    VALUES (@NombreUsuario, @PasswordHash);
                    SELECT LAST_INSERT_ID();";

        return await _dapper.QueryFirstAsync<int>(
            sql, 
            new { NombreUsuario = nombreUsuario, PasswordHash = passwordHash }, 
            CommandType.Text
        );
    }
}