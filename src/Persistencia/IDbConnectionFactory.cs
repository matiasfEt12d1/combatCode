using System.Data;

namespace Persistencia;

public interface IDbConnectionFactory
{
    IDbConnection CrearConexionDesarrollo();
    IDbConnection CrearConexionAdministrador();
}