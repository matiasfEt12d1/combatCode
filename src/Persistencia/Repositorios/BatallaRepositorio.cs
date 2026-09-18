using System.Data;
using Aplicacion.Interfaces;
using Persistencia.Entidades;

namespace Persistencia.Repositorios;

public class BatallaRepositorio : IBatallaRepositorio
{
    private readonly IConexionDapper _dapper;

    public BatallaRepositorio(IConexionDapper dapper)
    {
        _dapper = dapper;
    }

    public async Task<int> IniciarBatallaAsync(int atacanteId, int defensorId)
    {
        var parametros = new 
        { 
            p_AtacanteId = atacanteId, 
            p_DefensorId = defensorId 
        };

        return await _dapper.QueryFirstAsync<int>("sp_IniciarBatalla", parametros, CommandType.StoredProcedure);
    }

    public async Task CerrarBatallaTransaccionalAsync(int batallaId, int ganadorId, int perdedorId, double vidaRestanteGanador, int rondaFinal, double ultimoGolpe)
    {
        var parametros = new
        {
            p_BatallaId = batallaId,
            p_GanadorId = ganadorId,
            p_PerdedorId = perdedorId,
            p_VidaRestanteGanador = vidaRestanteGanador,
            p_RondaFinal = rondaFinal,
            p_DanioUltimoGolpe = ultimoGolpe
        };

        await _dapper.ExecuteAsync("sp_CerrarBatallaTransaccional", parametros, CommandType.StoredProcedure);
    }

    public async Task<BatallaTabla?> ObtenerPorIdAsync(int id)
    {
        var sql = "SELECT * FROM Batallas WHERE Id = @Id;";
        return await _dapper.QueryFirstAsync<BatallaTabla>(sql, new { Id = id }, CommandType.Text);
    }
}