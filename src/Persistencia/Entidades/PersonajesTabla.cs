namespace Persistencia.Entidades;

public class PersonajeTabla
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string TipoPersonaje { get; set; } = string.Empty;
    public double VidaMaxima { get; set; }
    public double VidaActual { get; set; }
    public double FuerzaBase { get; set; }
    public double? Armadura { get; set; }
    public int? ManaMaximo { get; set; }
    public int? ManaActual { get; set; }
    public int? CantidadFlechas { get; set; }
    public double? ProbabilidadCritico { get; set; }
}