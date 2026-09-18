using Aplicacion.Interfaces;
using Persistencia.Entidades;

namespace Aplicacion.Servicios;

public class ServicioPersonaje : IPersonajeServicio
{
    private readonly IPersonajeRepositorio _personajeRepo;

    public ServicioPersonaje(IPersonajeRepositorio personajeRepo)
    {
        _personajeRepo = personajeRepo;
    }

    public async Task<int> RegistrarPersonajeAsync(int usuarioId, string nombre, string tipo, double vidaMax, double fuerzaBase)
    {
        // Validaciones de negocio
        if (usuarioId <= 0) 
            throw new ArgumentException("El ID de usuario es inválido.");

        if (string.IsNullOrWhiteSpace(nombre)) 
            throw new ArgumentException("El nombre del personaje no puede estar vacío.");

        if (vidaMax <= 0 || fuerzaBase <= 0) 
            throw new ArgumentException("La vida máxima y la fuerza deben ser mayores a cero.");

        string tipoNormalizado = tipo.Trim().ToLower();
        var tiposValidos = new[] { "mago", "guerrero", "arquero", "asesino" };

        if (!tiposValidos.Contains(tipoNormalizado))
            throw new ArgumentException($"El tipo '{tipo}' no es válido. Tipos admitidos: Mago, Guerrero, Arquero, Asesino.");

        var nuevoPersonaje = new PersonajeTabla
        {
            UsuarioId = usuarioId,
            Nombre = nombre.Trim(),
            TipoPersonaje = tipoNormalizado,
            VidaMaxima = vidaMax,
            VidaActual = vidaMax,
            FuerzaBase = fuerzaBase,
            Armadura = tipoNormalizado == "guerrero" ? 15 : 0,
            ManaMaximo = tipoNormalizado == "mago" ? 100 : 0,
            ManaActual = tipoNormalizado == "mago" ? 100 : 0
        };

        return await _personajeRepo.CrearAsync(nuevoPersonaje);
    }

    public async Task<PersonajeTabla?> ObtenerPorIdAsync(int id)
    {
        if (id <= 0) 
            throw new ArgumentException("El ID ingresado es inválido.");

        return await _personajeRepo.ObtenerPorIdAsync(id);
    }

    public async Task<IEnumerable<PersonajeTabla>> ObtenerPorUsuarioIdAsync(int usuarioId)
    {
        if (usuarioId <= 0) 
            throw new ArgumentException("El ID de usuario ingresado es inválido.");

        return await _personajeRepo.ObtenerPorUsuarioIdAsync(usuarioId);
    }

    public async Task<bool> ActualizarSaludAsync(int personajeId, double nuevaVida)
    {
        if (personajeId <= 0) 
            throw new ArgumentException("El ID del personaje es inválido.");

        double vidaNormalizada = Math.Max(0, nuevaVida);
        return await _personajeRepo.ActualizarVidaAsync(personajeId, vidaNormalizada);
    }
}