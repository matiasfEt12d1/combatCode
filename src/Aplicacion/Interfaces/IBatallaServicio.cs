using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacion.Interfaces
{
    public class IBatallaServicio
    {
        void EjecutarRonda(Personaje atacante, Personaje defensor);
        Personaje IniciarCombate(Personaje combatiente1, Personaje combatiente2);
    }
}