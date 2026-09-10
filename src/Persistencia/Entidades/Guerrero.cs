namespace Persistencia.Entidades;

public class Guerrero : Personaje
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
        : base(nombre, vidaMaxima, fuerzaBase)
    {
        Armadura = armadura;
    }
    // Polimorfismo: Absorbe el daño utilizando su armadura sin afectar la firma del método base.
    public override void RecibirDano(double cantidad)
    {
        double danioEfectivo = Math.Max(0, cantidad - Armadura);
        base.RecibirDano(danioEfectivo);
    }
    public override double CalcularDanioAtaqueBasico()
    {
        if (!EstaVivo)
            throw new InvalidOperationException($"El guerrero {Nombre} está derrotado y no puede atacar.");
        return FuerzaBase * 1.2;
    }
    public override double UsarHabilidad(Habilidad habilidad)
    {
        if (!EstaVivo)
            throw new InvalidOperationException($"El guerrero {Nombre} está derrotado y no puede usar habilidades.");
        return CalcularDanioAtaqueBasico() + habilidad.PotenciaBase;
    }
}