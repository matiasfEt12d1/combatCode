using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Persistencia.Entidades
{
    public class Habilidad
    {
        private readonly string _nombre;
        private readonly int _costoRecurso;
        private readonly double _potenciaBase;
        
        public string Nombre => _nombre;
        public int CostoRecurso => _costoRecurso;
        public double PotenciaBase => _potenciaBase;


        public Habilidad(string nombre, int costoRecurso, double potenciaBase)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre de la habilidad no puede estar vacío.", nameof(nombre));

            if (costoRecurso < 0)
                throw new ArgumentOutOfRangeException(nameof(costoRecurso), "El costo de recurso no puede ser negativo.");

            if (potenciaBase <= 0)
                throw new ArgumentOutOfRangeException(nameof(potenciaBase), "La potencia base debe ser mayor a cero.");

            _nombre = nombre;
            _costoRecurso = costoRecurso;
            _potenciaBase = potenciaBase;
        }
    }
}