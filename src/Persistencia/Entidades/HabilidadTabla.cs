namespace Persistencia.Entidades;

public class HabilidadTabla
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string TipoPersonaje { get; set; } = string.Empty;
    public double DanioBase { get; set; }
    public int CostoMana { get; set; }
}