using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Persistencia.Entidades
{
    public class Personaje
    {
        private readonly string _nombre;
        private double _vidaActual;
        private readonly double _vidaMaxima;
        private double _fuerzaBase;
        private readonly List<Habilidad> _habilidades;

        public string Nombre => _nombre;
        public double VidaActual => _vidaActual;
        public double VidaMaxima => _vidaMaxima;
        public bool EstaVivo => _vidaActual > 0;

        public double FuerzaBase
        {
            get => _fuerzaBase;
            protected set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "La fuerza base debe ser mayor a cero.");
                _fuerzaBase = value;
            }
        }

        public IReadOnlyCollection<Habilidad> Habilidades => _habilidades.AsReadOnly();

        protected Personaje(string nombre, double vidaMaxima, double fuerzaBase)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre del personaje no puede estar vacío.", nameof(nombre));

            if (vidaMaxima <= 0)
                throw new ArgumentOutOfRangeException(nameof(vidaMaxima), "La vida máxima debe ser mayor a cero.");

            if (fuerzaBase <= 0)
                throw new ArgumentOutOfRangeException(nameof(fuerzaBase), "La fuerza base debe ser mayor a cero.");

            _nombre = nombre;
            _vidaMaxima = vidaMaxima;
            _vidaActual = vidaMaxima;
            _fuerzaBase = fuerzaBase;
            _habilidades = new List<Habilidad>();
        }

        public virtual void RecibirDano(double cantidad)
        {
            if (cantidad < 0)
                throw new ArgumentOutOfRangeException(nameof(cantidad), "El daño recibido no puede ser negativo.");

            _vidaActual = Math.Max(0, _vidaActual - cantidad);
        }

        public void Curar(double cantidad)
        {
            if (!EstaVivo)
                throw new InvalidOperationException($"El personaje {_nombre} está derrotado y no puede recibir curación.");

            if (cantidad < 0)
                throw new ArgumentOutOfRangeException(nameof(cantidad), "La cantidad a curar no puede ser negativa.");

            _vidaActual = Math.Min(_vidaMaxima, _vidaActual + cantidad);
        }

        public void AgregarHabilidad(Habilidad habilidad)
        {
            ArgumentNullException.ThrowIfNull(habilidad);
            _habilidades.Add(habilidad);
        }

        public abstract double CalcularDanioAtaqueBasico();
        public abstract double UsarHabilidad(Habilidad habilidad);

    }
}