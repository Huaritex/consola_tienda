using System;

namespace tienda_consola
{
    class Program
    {
        static void Main(string[] args)
        {
            Inventario inventario = new Inventario();
            inventario.AgregarProducto("PlayStation 5", 10, 500);
            inventario.AgregarProducto("Xbox Series X", 8, 500);
            inventario.AgregarProducto("Nintendo Switch", 15, 300);

            Carrito carrito = new Carrito();

            int opcion = 0;

            while (opcion != 4)
            {
                Console.WriteLine("\nTIENDA DE CONSOLAS");
                Console.WriteLine("1. Ver productos");
                Console.WriteLine("2. Agregar al carrito");
                Console.WriteLine("3. Ver carrito");
                Console.WriteLine("4. Salir");
                Console.Write("Elige una opcion: ");

                opcion = int.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        inventario.MostrarProductos();
                        break;

                    case 2:
                        inventario.MostrarProductos();
                        Console.Write("Elige el numero del producto: ");
                        int numProducto = int.Parse(Console.ReadLine());

                        if (numProducto >= 1 && numProducto <= inventario.total)
                        {
                            Console.Write("¿Cuántos quieres? ");
                            int cantidad = int.Parse(Console.ReadLine());

                            Producto elegido = inventario.productos[numProducto - 1];

                            if (cantidad <= elegido.cantidad)
                            {
                                carrito.Agregar(elegido, cantidad);
                                elegido.cantidad -= cantidad; 
                            }
                            else
                            {
                                Console.WriteLine("No hay suficiente stock.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Producto no válido.");
                        }
                        break;

                    case 3:
                        carrito.MostrarCarrito();
                        break;

                    case 4:
                        Console.WriteLine("¡Gracias por tu compra!");
                        break;

                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }
            }
        }
    }
}