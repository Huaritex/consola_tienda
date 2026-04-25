using System;

namespace tienda_consola
{
    public abstract class Descuento
    {
        public string Nombre { get; set; }
        public Descuento(string nombre) { Nombre = nombre; }
        public abstract double Calcular(double subtotal);
    }

    public class DescuentoFijo : Descuento
    {
        public double Porcentaje { get; set; }
        public DescuentoFijo(string nombre, double porcentaje) : base(nombre)
        {
            Porcentaje = porcentaje;
        }
        public override double Calcular(double subtotal)
        {
            return subtotal * (Porcentaje / 100.0);
        }
    }

    public class DescuentoVolumen : Descuento
    {
        public double MontoMinimo { get; set; }
        public double Porcentaje { get; set; }
        public DescuentoVolumen(string nombre, double montoMinimo, double porcentaje) : base(nombre)
        {
            MontoMinimo = montoMinimo;
            Porcentaje = porcentaje;
        }
        public override double Calcular(double subtotal)
        {
            if (subtotal > MontoMinimo)
                return subtotal * (Porcentaje / 100.0);
            return 0;
        }
    }
}
