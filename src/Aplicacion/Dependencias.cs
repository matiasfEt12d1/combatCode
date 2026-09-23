using Microsoft.Extensions.DependencyInjection;
using Aplicacion.Interfaces;
using Aplicacion.Servicios;
using Persistencia;
using Persistencia.Repositorios;

namespace Aplicacion;

public static class Dependencias
{
    public static IServiceCollection AgregarServiciosYPersistencia(
        this IServiceCollection services, 
        string conexionDev, 
        string conexionAdmin)
    {
        // Conexión
        services.AddSingleton<IDbConnectionFactory>(_ => 
            new MySqlConnectionFactory(conexionDev, conexionAdmin));

        // Repositorios
        services.AddScoped<IPersonajeRepositorio, PersonajeRepositorio>();
        services.AddScoped<IBatallaRepositorio, BatallaRepositorio>();
        services.AddScoped<IHabilidadRepositorio, HabilidadRepositorio>();
        services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();

        // Servicios
        services.AddScoped<IPersonajeServicio, ServicioPersonaje>();
        services.AddScoped<IBatallaServicio, ServicioBatalla>();

        return services;
    }
}