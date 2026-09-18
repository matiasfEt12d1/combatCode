using Persistencia.Entidades;

namespace Test;

public class PersonajeTests
{
    [Fact]
    public void Guerrero_RecibirDano_MitigaDanoConArmadura()
    {
        var guerrero = new Guerrero("Thor", 100, 20, 15);

        guerrero.RecibirDano(40);

        Assert.Equal(75, guerrero.VidaActual);
    }

    [Fact]
    public void Mago_UsarHabilidad_ConsumeManaYMultiplicaPotencia()
    {
        var mago = new Mago("Gandalf", 80, 10, 100);
        var habilidad = new Habilidad("Bola de Fuego", 30, 50);

        double danio = mago.UsarHabilidad(habilidad);

        Assert.Equal(75, danio);
        Assert.Equal(70, mago.ManaActual);
    }

    [Fact]
    public void Mago_UsarHabilidad_SinManaSuficiente_LanzaExcepcion()
    {
        var mago = new Mago("Gandalf", 80, 10, 15);
        var habilidad = new Habilidad("Rayo", 30, 50);

        Assert.Throws<InvalidOperationException>(() => mago.UsarHabilidad(habilidad));
    }

    [Fact]
    public void Arquero_CalcularDanioAtaqueBasico_ConsumeFlechaYReduceDanioSinProyectiles()
    {
        var arquero = new Arquero("Legolas", 90, 10, 1);

        double danioConFlecha = arquero.CalcularDanioAtaqueBasico();
        
        double danioSinFlecha = arquero.CalcularDanioAtaqueBasico();

        Assert.Equal(14, danioConFlecha);
        Assert.Equal(5, danioSinFlecha);
        Assert.Equal(0, arquero.CantidadFlechas);
    }

    [Fact]
    public void Asesino_UsarHabilidad_AplicaFactorMultiplicadorCorrecto()
    {
        var asesino = new Asesino("Ezio", 70, 20, 0.25);
        var habilidad = new Habilidad("Ataque Sombra", 0, 40);

        double danio = asesino.UsarHabilidad(habilidad);

        Assert.Equal(105, danio);
    }

    [Fact]
    public void Personaje_Curar_NoExcedeVidaMaxima()
    {
        var guerrero = new Guerrero("Conan", 100, 20, 5);
        guerrero.RecibirDano(30); // Queda con 75 (30 - 5 de armadura = 25 de daño)

        guerrero.Curar(50);

        Assert.Equal(100, guerrero.VidaActual);
    }

    [Fact]
    public void Personaje_Curar_EstandoDerrotado_LanzaExcepcion()
    {
        var mago = new Mago("Merlín", 50, 10, 50);
        mago.RecibirDano(100);

        Assert.Throws<InvalidOperationException>(() => mago.Curar(20));
    }
}