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
    {
        var conexion = new MySqlConnection(_cadenaDesarrollo);
        conexion.Open();
        return conexion;
    }

    public IDbConnection CrearConexionAdministrador() 
    {
        var conexion = new MySqlConnection(_cadenaAdministrador);
        conexion.Open();
        return conexion;
    }
}