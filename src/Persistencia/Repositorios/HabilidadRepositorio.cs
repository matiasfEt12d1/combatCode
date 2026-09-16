using System.Data;
using Persistencia.Entidades;

namespace Persistencia.Repositorios;

public class HabilidadRepositorio : IHabilidadRepositorio
{
    private readonly IConexionDapper _dapper;

    public HabilidadRepositorio(IConexionDapper dapper)
    {
        _dapper = dapper;
    }

    public async Task<IEnumerable<HabilidadTabla>> ObtenerPorTipoPersonajeAsync(string tipoPersonaje)
    {
        var sql = "SELECT * FROM Habilidades WHERE TipoPersonaje = @TipoPersonaje OR TipoPersonaje = 'Todos';";
        return await _dapper.QueryAsync<HabilidadTabla>(sql, new { TipoPersonaje = tipoPersonaje }, CommandType.Text);
    }

    public async Task<HabilidadTabla?> ObtenerPorIdAsync(int id)
    {
        var sql = "SELECT * FROM Habilidades WHERE Id = @Id;";
        return await _dapper.QueryFirstAsync<HabilidadTabla>(sql, new { Id = id }, CommandType.Text);
    }
}