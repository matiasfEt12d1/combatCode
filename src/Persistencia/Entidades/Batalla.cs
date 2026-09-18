using Persistencia.Entidades;

namespace Dominio.Entidades;

public class Batalla
{
    private readonly Random _random = new();

    public int Id { get; private set; }
    public Personaje Atacante { get; private set; }
    public Personaje Defensor { get; private set; }
    public DateTime FechaInicio { get; private set; }
    public int TotalRondas { get; private set; }
    public double UltimoGolpe { get; private set; }
    public Personaje? Ganador { get; private set; }
    public Personaje? Perdedor { get; private set; }
    public bool Finalizada { get; private set; }

    public Batalla(int id, Personaje atacante, Personaje defensor)
    {
        ArgumentNullException.ThrowIfNull(atacante);
        ArgumentNullException.ThrowIfNull(defensor);

        if (!atacante.EstaVivo || !defensor.EstaVivo)
            throw new InvalidOperationException("Ambos personajes deben estar vivos para iniciar la batalla.");

        Id = id;
        Atacante = atacante;
        Defensor = defensor;
        FechaInicio = DateTime.UtcNow;
        TotalRondas = 0;
        Finalizada = false;
    }

    public void SimularCombate()
    {
        if (Finalizada)
            throw new InvalidOperationException("La batalla ya ha finalizado.");

        var p1 = Atacante;
        var p2 = Defensor;

        while (p1.EstaVivo && p2.EstaVivo)
        {
            TotalRondas++;

            EjecutarTurno(p1, p2);
            if (!p2.EstaVivo) break;

            EjecutarTurno(p2, p1);
        }

        Finalizada = true;

        if (p1.EstaVivo)
        {
            Ganador = p1;
            Perdedor = p2;
        }
        else
        {
            Ganador = p2;
            Perdedor = p1;
        }
    }

    private void EjecutarTurno(Personaje atacante, Personaje defensor)
    {
        double danio;

        if (atacante.Habilidades.Count > 0 && _random.NextDouble() < 0.4)
        {
            var habilidad = atacante.Habilidades.ElementAt(_random.Next(atacante.Habilidades.Count));
            danio = atacante.UsarHabilidad(habilidad);
        }
        else
        {
            danio = atacante.CalcularDanioAtaqueBasico();
        }

        UltimoGolpe = danio;
        defensor.RecibirDano(danio);
    }
}