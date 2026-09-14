using System.Data;
using MySqlConnector;

namespace Persistencia;

public class MySqlConnectionFactory : IDbConnectionFactory
{
    private readonly string _cadenaDesarrollo;
    private readonly string _cadenaAdministrador;

    public MySqlConnectionFactory(string cadenaDesarrollo, string cadenaAdministrador)
    {
        _cadenaDesarrollo = cadenaDesarrollo;
        _cadenaAdministrador = cadenaAdministrador;
    }

    public IDbConnection CrearConexionDesarrollo() 
        => new MySqlConnection(_cadenaDesarrollo);

    public IDbConnection CrearConexionAdministrador() 
        => new MySqlConnection(_cadenaAdministrador);
}