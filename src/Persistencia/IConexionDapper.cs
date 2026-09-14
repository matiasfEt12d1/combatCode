using System.Data;

namespace Persistencia;

public interface IConexionDapper
{
    Task<int> ExecuteAsync(string sql, object? param = null, CommandType commandType = CommandType.StoredProcedure);
    Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param = null, CommandType commandType = CommandType.StoredProcedure);
    Task<T> QueryFirstAsync<T>(string sql, object? param = null, CommandType commandType = CommandType.StoredProcedure);
}