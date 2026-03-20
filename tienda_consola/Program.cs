using System;

namespace tienda_consola
{
    class Program
    {
        static void Main(string[] args)
        {
            Usuario admin = new Usuario("admin", "1234");
            bool autenticado = false;

            while (!autenticado)
            {
                Console.WriteLine("\n--- LOGIN ---");
                Console.Write("Usuario: ");
                string user = Console.ReadLine();
                Console.Write("Contraseña: ");
                string pass = Console.ReadLine();

                if (admin.iniciar_Sesion(user, pass))
                {
                    Console.WriteLine("¡Bienvenido, " + admin.name_usuario + "!");
                    autenticado = true;
                }
                else
                {
                    Console.WriteLine("Usuario o contraseña incorrectos. Intente de nuevo.");
                }
            }

            Inventario inventario_1 = new Inventario();
            inventario_1.AgregarProducto("PlayStation 5", 10, 500);
            inventario_1.AgregarProducto("Xbox Series X", 8, 500);
            inventario_1.AgregarProducto("Nintendo Switch", 15, 300);
            inventario_1.AgregarProducto("Steam Deck", 5, 450);
            inventario_1.AgregarProducto("PlayStation 4", 20, 250);
            inventario_1.AgregarProducto("Xbox One", 15, 200);
            inventario_1.AgregarProducto("Nintendo Switch Lite", 10, 200);
            inventario_1.AgregarProducto("Meta Quest 3", 8, 500);
            inventario_1.AgregarProducto("PlayStation VR2", 6, 550);
            inventario_1.AgregarProducto("ASUS ROG Ally", 4, 700);

            Presentacion_tienda presentacion = new Presentacion_tienda();
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

                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    Console.WriteLine("Entrada no válida. Ingresa un número.");
                    continue;
                }

                switch (opcion)
                {
                    case 1:
                        presentacion.MP_Inventario(inventario_1);
                        break;

                    case 2:
                        presentacion.MP_Inventario(inventario_1);
                        Console.Write("Elige el numero del producto: ");
                        if (!int.TryParse(Console.ReadLine(), out int numProducto))
                        {
                            Console.WriteLine("Entrada no válida.");
                            break;
                        }

                        if (numProducto >= 1 && numProducto <= inventario_1.total)
                        {
                            Console.Write("¿Cuántos quieres? ");
                            if (!int.TryParse(Console.ReadLine(), out int cantidad))
                            {
                                Console.WriteLine("Entrada no válida.");
                                break;
                            }

                            Producto elegido = inventario_1.productos[numProducto - 1];

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