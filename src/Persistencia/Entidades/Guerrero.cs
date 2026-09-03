namespace Persistencia.Entidades;

public class Guerrero
{
    private double _armadura;

    public double Armadura
    {
        get => _armadura;
        private set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value), "La armadura no puede ser negativa.");
            _armadura = value;
        }
    }

    public Guerrero(string nombre, double vidaMaxima, double fuerzaBase, double armadura)
        : base(nombre, vidaMaxima, fuerzaBase) => Armadura = armadura;

    public override void RecibirDano(double cantidad)
    {
        double danioEfectivo = Math.Max(0, cantidad - Armadura);
        base.RecibirDano(danioEfectivo);
    }

    public override double CalcularDanioAtaqueBásico() => FuerzaBase * 1.2;

    public override double UsarHabilidad(Habilidad habilidad) => CalcularDanioAtaqueBásico() + habilidad.PotenciaBase;
}