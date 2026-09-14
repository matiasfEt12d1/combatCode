using System.Data;
using Dapper;

namespace Persistencia;

public class ConexionDapper : IConexionDapper
{
    private readonly IDbConnectionFactory _connectionFactory;
    private readonly bool _usarAdmin;

    public ConexionDapper(IDbConnectionFactory connectionFactory, bool usarAdmin = false)
    {
        _connectionFactory = connectionFactory;
        _usarAdmin = usarAdmin;
    }

    private IDbConnection ObtenerConexion() => _usarAdmin 
        ? _connectionFactory.CrearConexionAdministrador() 
        : _connectionFactory.CrearConexionDesarrollo();

    public async Task<int> ExecuteAsync(string sql, object? param = null, CommandType commandType = CommandType.StoredProcedure)
    {
        using var db = ObtenerConexion();
        return await db.ExecuteAsync(sql, param, commandType: commandType);
    }

    public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param = null, CommandType commandType = CommandType.StoredProcedure)
    {
        using var db = ObtenerConexion();
        return await db.QueryAsync<T>(sql, param, commandType: commandType);
    }

    public async Task<T> QueryFirstAsync<T>(string sql, object? param = null, CommandType commandType = CommandType.StoredProcedure)
    {
        using var db = ObtenerConexion();
        return await db.QueryFirstAsync<T>(sql, param, commandType: commandType);
    }
}