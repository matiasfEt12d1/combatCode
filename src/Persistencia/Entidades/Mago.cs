namespace Persistencia.Entidades;

public class Mago
{
    private int _manaActual;
    private readonly int _manaMaximo;

    public int ManaActual => _manaActual;

    public Mago(string nombre, double vidaMaxima, double fuerzaBase, int manaMaximo)
        : base(nombre, vidaMaxima, fuerzaBase)
    {
        if (manaMaximo <= 0)
            throw new ArgumentOutOfRangeException(nameof(manaMaximo), "El mana máximo debe ser mayor a cero.");

        _manaMaximo = manaMaximo;
        _manaActual = manaMaximo;
    }

    public override double CalcularDanioAtaqueBásico() => FuerzaBase * 0.8;

    public override double UsarHabilidad(Habilidad habilidad)
    {
        if (_manaActual < habilidad.CostoRecurso)
            throw new InvalidOperationException($"Mana insuficiente para usar {habilidad.Nombre}.");

        _manaActual -= habilidad.CostoRecurso;
        return habilidad.PotenciaBase * 1.5;
    }
}