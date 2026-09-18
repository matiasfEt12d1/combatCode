using System.Data;
using Aplicacion.Interfaces;
using Persistencia.Entidades;

namespace Persistencia.Repositorios;

public class PersonajeRepositorio : IPersonajeRepositorio
{
    private readonly IConexionDapper _dapper;

    public PersonajeRepositorio(IConexionDapper dapper)
    {
        _dapper = dapper;
    }

    public async Task<PersonajeTabla?> ObtenerPorIdAsync(int id)
    {
        var sql = "SELECT * FROM Personajes WHERE Id = @Id;";
        
        return await _dapper.QueryFirstAsync<PersonajeTabla>(sql, new { Id = id }, CommandType.Text);
    }

    public async Task<IEnumerable<PersonajeTabla>> ObtenerPorUsuarioIdAsync(int usuarioId)
    {
        var sql = "SELECT * FROM Personajes WHERE UsuarioId = @UsuarioId;";
        
        return await _dapper.QueryAsync<PersonajeTabla>(sql, new { UsuarioId = usuarioId }, CommandType.Text);
    }

    public async Task<IEnumerable<PersonajeTabla>> ObtenerTodosAsync()
    {
        var sql = "SELECT * FROM Personajes;";
        
        return await _dapper.QueryAsync<PersonajeTabla>(sql, null, CommandType.Text);
    }

    public async Task<int> CrearAsync(PersonajeTabla personaje)
    {
        var parametros = new
        {
            p_UsuarioId = personaje.UsuarioId,
            p_Nombre = personaje.Nombre,
            p_TipoPersonaje = personaje.TipoPersonaje,
            p_VidaMaxima = personaje.VidaMaxima,
            p_FuerzaBase = personaje.FuerzaBase,
            p_Armadura = personaje.Armadura,
            p_ManaMaximo = personaje.ManaMaximo,
            p_CantidadFlechas = personaje.CantidadFlechas,
            p_ProbabilidadCritico = personaje.ProbabilidadCritico
        };

        return await _dapper.ExecuteAsync("sp_RegistrarPersonaje", parametros, CommandType.StoredProcedure);
    }

    public async Task<bool> ActualizarVidaAsync(int personajeId, double nuevaVida)
    {
        var sql = "UPDATE Personajes SET VidaActual = @VidaActual WHERE Id = @Id;";
        
        var filasAfectadas = await _dapper.ExecuteAsync(
            sql, 
            new { Id = personajeId, VidaActual = nuevaVida }, 
            CommandType.Text
        );

        return filasAfectadas > 0;
    }
}