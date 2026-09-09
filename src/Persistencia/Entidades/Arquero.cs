namespace Persistencia.Entidades;

public class Arquero
{
    private int _cantidadFlechas;

    public int CantidadFlechas => _cantidadFlechas;

    public Arquero(string nombre, double vidaMaxima, double fuerzaBase, int flechasIniciales)
        : base(nombre, vidaMaxima, fuerzaBase)
    {
        if (flechasIniciales < 0)
            throw new ArgumentOutOfRangeException(nameof(flechasIniciales), "La cantidad de flechas no puede ser negativa.");

        _cantidadFlechas = flechasIniciales;
    }

    public override double CalcularDanioAtaqueBásico()
    {
        if (_cantidadFlechas <= 0)
            return FuerzaBase * 0.5; // Ataque debilitado sin flechas

        _cantidadFlechas--;
        return FuerzaBase * 1.4;
    }

    public override double UsarHabilidad(Habilidad habilidad)
    {
        if (_cantidadFlechas < habilidad.CostoRecurso)
            throw new InvalidOperationException("No hay suficientes flechas para ejecutar esta habilidad.");

        _cantidadFlechas -= habilidad.CostoRecurso;
        return FuerzaBase + habilidad.PotenciaBase;
    }
}