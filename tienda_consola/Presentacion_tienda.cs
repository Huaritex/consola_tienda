using System;

namespace tienda_consola
{
    public class Presentacion_tienda
    {
        public void MP_Inventario(Inventario inventario)
        {
            Console.WriteLine("\nStock de Productos del Inventario");
            for (int i = 0; i < inventario.total; i++)
            {
                Producto p = inventario.productos[i];
                Console.WriteLine($"{i + 1}. {p.nombre} \t| Stock: {p.cantidad} \t| Precio: {p.precio:C}");
            }
        }
    }
}
