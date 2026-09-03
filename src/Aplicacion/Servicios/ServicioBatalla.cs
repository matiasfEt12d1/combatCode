using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacion.Servicios
{
    public class ServicioBatalla : IBatallaServicio
    {
        public void EjecutarRonda(Personaje atacante, Personaje defensor)
        {
            ArgumentNullException.ThrowIfNull(atacante);
            ArgumentNullException.ThrowIfNull(defensor);

            if (!atacante.EstaVivo)
                throw new InvalidOperationException($"El atacante {atacante.Nombre} no puede luchar porque está derrotado.");

            if (!defensor.EstaVivo)
                throw new InvalidOperationException($"El defensor {defensor.Nombre} ya ha sido derrotado.");

            double danio = atacante.CalcularDanioAtaqueBásico();
            defensor.RecibirDano(danio);
        }

        public Personaje IniciarCombate(Personaje combatiente1, Personaje combatiente2)
        {
            ArgumentNullException.ThrowIfNull(combatiente1);
            ArgumentNullException.ThrowIfNull(combatiente2);

            if (ReferenceEquals(combatiente1, combatiente2))
                throw new ArgumentException("Un personaje no puede luchar contra sí mismo.");

            int turno = 0;
            while (combatiente1.EstaVivo && combatiente2.EstaVivo)
            {
                if (turno % 2 == 0)
                    EjecutarRonda(combatiente1, combatiente2);
                else
                    EjecutarRonda(combatiente2, combatiente1);

                turno++;
            }

            return combatiente1.EstaVivo ? combatiente1 : combatiente2;
        }
    }
}