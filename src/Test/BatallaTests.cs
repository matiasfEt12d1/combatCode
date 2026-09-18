using Dominio.Entidades;
using Persistencia.Entidades;

namespace Test;

public class BatallaTests
{
    [Fact]
    public void Constructor_PersonajeMuerto_LanzaExcepcion()
    {
        var vivo = new Guerrero("Conan", 100, 20, 10);
        var muerto = new Mago("Gandalf", 50, 10, 50);
        muerto.RecibirDano(100);

        Assert.Throws<InvalidOperationException>(() => new Batalla(1, vivo, muerto));
    }

    [Fact]
    public void SimularCombate_ResuelveTurnosHastaDerrotarUnCombatiente()
    {
        var guerrero = new Guerrero("Conan", 150, 25, 10);
        var mago = new Mago("Gandalf", 20, 10, 50);
        var batalla = new Batalla(1, guerrero, mago);

        batalla.SimularCombate();

        Assert.True(batalla.Finalizada);
        Assert.NotNull(batalla.Ganador);
        Assert.NotNull(batalla.Perdedor);
        Assert.True(batalla.TotalRondas > 0);
        Assert.True(batalla.UltimoGolpe > 0);
        Assert.True(batalla.Ganador!.EstaVivo);
        Assert.False(batalla.Perdedor!.EstaVivo);
    }

    [Fact]
    public void SimularCombate_BatallaYaFinalizada_LanzaExcepcion()
    {
        var guerrero = new Guerrero("Conan", 100, 20, 5);
        var asesino = new Asesino("Ezio", 30, 15, 0.0);
        var batalla = new Batalla(1, guerrero, asesino);

        batalla.SimularCombate(); // Finaliza en la primera simulación

        Assert.Throws<InvalidOperationException>(() => batalla.SimularCombate());
    }

    [Fact]
    public void SimularCombate_GarantizaActualizacionDeEstadoYUltimoGolpe()
    {
        var arquero = new Arquero("Robin", 100, 20, 5);
        var guerrero = new Guerrero("Ragnar", 100, 15, 2);
        var batalla = new Batalla(99, arquero, guerrero);

        batalla.SimularCombate();

        Assert.True(batalla.UltimoGolpe > 0);
        Assert.True(batalla.FechaInicio <= DateTime.UtcNow);
    }
}