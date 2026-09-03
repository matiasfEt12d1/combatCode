using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Persistencia.Entidades
{
    public class Asesino
    {
        private double _probabilidadCritico;

        public Asesino(string nombre, double vidaMaxima, double fuerzaBase, double probabilidadCritico)
            : base(nombre, vidaMaxima, fuerzaBase)
        {
            if (probabilidadCritico < 0 || probabilidadCritico > 1.0)
                throw new ArgumentOutOfRangeException(nameof(probabilidadCritico), "La probabilidad de crítico debe ser entre 0 y 1.");

            _probabilidadCritico = probabilidadCritico;
        }

        public override double CalcularDanioAtaqueBásico()
        {
            bool esCritico = Random.Shared.NextDouble() <= _probabilidadCritico;
            return esCritico ? FuerzaBase * 2.0 : FuerzaBase;
        }

        public override double UsarHabilidad(Habilidad habilidad) => (FuerzaBase + habilidad.PotenciaBase) * 1.75;
    }
}