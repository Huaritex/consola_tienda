using System;

namespace tienda_consola
{
    public class Presentacion_tienda
    {
        private mostrar_inventario inventario_simple;

        public Presentacion_tienda()
        {
            inventario_simple = new mostrar_inventario();
        }

        public void Ejecutar()
        {
            TADLista<string> permisos_admin = new TADLista<string>();
            permisos_admin.Agregar("ver_productos");
            permisos_admin.Agregar("comprar");

            TADLista<string> permisos_cliente = new TADLista<string>();
            permisos_cliente.Agregar("ver_productos");
            permisos_cliente.Agregar("comprar");

            Rol rol_admin = new Rol("admin", permisos_admin);
            Rol rol_cliente = new Rol("cliente", permisos_cliente);

            Usuario usuario_admin = new Usuario("admin", "1234", rol_admin);

            TADLista<Usuario> lista_usuarios = new TADLista<Usuario>();
            lista_usuarios.Agregar(usuario_admin);
            lista_usuarios.Agregar(new Usuario("cliente", "1234", rol_cliente));

            Inventario inventario_principal = new Inventario();
            inventario_principal.AgregarProducto("PlayStation 5", 10, 500);
            inventario_principal.AgregarProducto("Xbox Series X", 8, 500);
            inventario_principal.AgregarProducto("Nintendo Switch", 15, 300);
            inventario_principal.AgregarProducto("Steam Deck", 5, 450);
            inventario_principal.AgregarProducto("PlayStation 4", 20, 250);
            inventario_principal.AgregarProducto("Xbox One", 15, 200);
            inventario_principal.AgregarProducto("Nintendo Switch Lite", 10, 200);
            inventario_principal.AgregarProducto("Meta Quest 3", 8, 500);
            inventario_principal.AgregarProducto("PlayStation VR2", 6, 550);
            inventario_principal.AgregarProducto("ASUS ROG Ally", 4, 700);

            Carrito carrito_compra = new Carrito();
            bool tienda_cerrada = false;

            while (!tienda_cerrada)
            {
                Usuario usuario_en_sesion = IniciarLogin(lista_usuarios);
                bool sesion_cerrada = false;

                if (usuario_en_sesion.rol.es_admin())
                {
                    while (!sesion_cerrada && !tienda_cerrada)
                    {
                        Console.WriteLine("\nMENU ADMINISTRADOR");
                        Console.WriteLine("1. Listar productos");
                        Console.WriteLine("2. Agregar producto");
                        Console.WriteLine("3. Actualizar producto");
                        Console.WriteLine("4. Eliminar producto");
                        Console.WriteLine("5. Listar usuarios");
                        Console.WriteLine("6. Agregar usuario");
                        Console.WriteLine("7. Actualizar usuario");
                        Console.WriteLine("8. Eliminar usuario");
                        Console.WriteLine("9. Cerrar sesion");
                        Console.WriteLine("10. Cerrar tienda");
                        int opcion_admin = LeerNumero("Elige una opcion: ");

                        switch (opcion_admin)
                        {
                            case 1:
                                MP_Inventario(inventario_principal);
                                break;

                            case 2:
                                RegistrarProducto(inventario_principal);
                                break;

                            case 3:
                                EditarProducto(inventario_principal);
                                break;

                            case 4:
                                QuitarProducto(inventario_principal);
                                break;

                            case 5:
                                MostrarUsuarios(lista_usuarios);
                                break;

                            case 6:
                                RegistrarUsuario(lista_usuarios, rol_admin, rol_cliente);
                                break;

                            case 7:
                                EditarUsuario(lista_usuarios, rol_admin, rol_cliente);
                                break;

                            case 8:
                                QuitarUsuario(lista_usuarios, usuario_en_sesion);
                                break;

                            case 9:
                                usuario_en_sesion.cerrar_Sesion();
                                sesion_cerrada = true;
                                break;

                            case 10:
                                tienda_cerrada = true;
                                break;

                            default:
                                Console.WriteLine("Opción no válida.");
                                break;
                        }
                    }
                }
                else
                {
                    while (!sesion_cerrada && !tienda_cerrada)
                    {
                        Console.WriteLine("\nMENU CLIENTE");
                        Console.WriteLine("1. Ver productos disponibles");
                        Console.WriteLine("2. Realizar una compra");
                        Console.WriteLine("3. Cerrar sesion");
                        Console.WriteLine("4. Cerrar tienda");
                        int opcion_cliente = LeerNumero("Elige una opcion: ");

                        switch (opcion_cliente)
                        {
                            case 1:
                                MP_Inventario(inventario_principal);
                                break;

                            case 2:
                                HacerCompra(inventario_principal, carrito_compra);
                                break;

                            case 3:
                                usuario_en_sesion.cerrar_Sesion();
                                sesion_cerrada = true;
                                break;

                            case 4:
                                tienda_cerrada = true;
                                break;

                            default:
                                Console.WriteLine("Opción no válida.");
                                break;
                        }
                    }
                }
            }
        }

        public void MP_Inventario(Inventario inventario)
        {
            Console.WriteLine("\nStock de Productos del Inventario");
            for (int i = 0; i < inventario.total; i++)
            {
                Producto p = inventario.productos.Obtener(i);
                Console.WriteLine($"{i + 1}. {p.nombre} \t| Stock: {p.cantidad} \t| Precio: {p.precio:C}");
            }
        }

        private Usuario IniciarLogin(TADLista<Usuario> lista_usuarios)
        {
            while (true)
            {
                Console.WriteLine("\n--- LOGIN ---");
                Console.Write("Usuario: ");
                string usuario_ingresado = Console.ReadLine();
                Console.Write("Contraseña: ");
                string clave_ingresada = Console.ReadLine();

                for (int i = 0; i < lista_usuarios.Cantidad; i++)
                {
                    Usuario usuario = lista_usuarios.Obtener(i);
                    if (usuario.iniciar_Sesion(usuario_ingresado, clave_ingresada))
                    {
                        return usuario;
                    }
                }

                Console.WriteLine("Usuario o contraseña incorrectos. Intente de nuevo.");
            }
        }

        private void RegistrarProducto(Inventario inventario)
        {
            string nombre_producto = LeerCadena("Nombre del producto: ");
            int cantidad_producto = LeerNumero("Cantidad: ");
            double precio_producto = LeerDecimal("Precio: ");
            inventario.AgregarProducto(nombre_producto, cantidad_producto, precio_producto);
            Console.WriteLine("Producto agregado.");
        }

        private void EditarProducto(Inventario inventario)
        {
            if (inventario.total == 0)
            {
                Console.WriteLine("No hay productos.");
                return;
            }

            inventario_simple.MostrarInventarioBasico(inventario);
            int indice_producto = LeerNumero("Numero del producto a actualizar: ") - 1;
            if (indice_producto < 0 || indice_producto >= inventario.total)
            {
                Console.WriteLine("Producto no válido.");
                return;
            }

            Producto producto_actual = inventario.ObtenerProducto(indice_producto);
            producto_actual.nombre = LeerCadena("Nuevo nombre: ");
            producto_actual.cantidad = LeerNumero("Nueva cantidad: ");
            producto_actual.precio = LeerDecimal("Nuevo precio: ");
            Console.WriteLine("Producto actualizado.");
        }

        private void QuitarProducto(Inventario inventario)
        {
            if (inventario.total == 0)
            {
                Console.WriteLine("No hay productos.");
                return;
            }

            inventario_simple.MostrarInventarioBasico(inventario);
            int indice_producto = LeerNumero("Numero del producto a eliminar: ") - 1;
            if (indice_producto < 0 || indice_producto >= inventario.total)
            {
                Console.WriteLine("Producto no válido.");
                return;
            }

            inventario.EliminarProducto(indice_producto);
            Console.WriteLine("Producto eliminado.");
        }

        private void MostrarUsuarios(TADLista<Usuario> usuarios)
        {
            Console.WriteLine("\nUsuarios:");
            for (int i = 0; i < usuarios.Cantidad; i++)
            {
                Usuario usuario = usuarios.Obtener(i);
                Console.WriteLine((i + 1) + ". " + usuario.name_usuario + " | Rol: " + usuario.rol.ObtenerTipo());
            }
        }

        private void RegistrarUsuario(TADLista<Usuario> usuarios, Rol rol_admin, Rol rol_cliente)
        {
            string nombre_usuario = LeerCadena("Nombre de usuario: ");
            if (BuscarUsuarioPorNombre(usuarios, nombre_usuario) != null)
            {
                Console.WriteLine("El usuario ya existe.");
                return;
            }

            string clave_usuario = LeerCadena("Contraseña: ");
            Rol rol_usuario = SeleccionarRol(rol_admin, rol_cliente);
            usuarios.Agregar(new Usuario(nombre_usuario, clave_usuario, rol_usuario));
            Console.WriteLine("Usuario agregado.");
        }

        private void EditarUsuario(TADLista<Usuario> usuarios, Rol rol_admin, Rol rol_cliente)
        {
            string nombre_usuario = LeerCadena("Usuario a actualizar: ");
            Usuario usuario_encontrado = BuscarUsuarioPorNombre(usuarios, nombre_usuario);
            if (usuario_encontrado == null)
            {
                Console.WriteLine("Usuario no encontrado.");
                return;
            }

            usuario_encontrado.pwd = LeerCadena("Nueva contraseña: ");
            usuario_encontrado.rol = SeleccionarRol(rol_admin, rol_cliente);
            Console.WriteLine("Usuario actualizado.");
        }

        private void QuitarUsuario(TADLista<Usuario> usuarios, Usuario usuario_en_sesion)
        {
            string nombre_usuario = LeerCadena("Usuario a eliminar: ");
            Usuario usuario_encontrado = BuscarUsuarioPorNombre(usuarios, nombre_usuario);
            if (usuario_encontrado == null)
            {
                Console.WriteLine("Usuario no encontrado.");
                return;
            }

            if (usuario_encontrado == usuario_en_sesion)
            {
                Console.WriteLine("No puedes eliminar el usuario en sesión.");
                return;
            }

            int indice_usuario = -1;
            for (int i = 0; i < usuarios.Cantidad; i++)
            {
                if (usuarios.Obtener(i) == usuario_encontrado)
                {
                    indice_usuario = i;
                    break;
                }
            }

            if (indice_usuario >= 0)
            {
                usuarios.EliminarEn(indice_usuario);
                Console.WriteLine("Usuario eliminado.");
            }
        }

        private Usuario BuscarUsuarioPorNombre(TADLista<Usuario> usuarios, string nombre_usuario)
        {
            for (int i = 0; i < usuarios.Cantidad; i++)
            {
                Usuario usuario = usuarios.Obtener(i);
                if (usuario.name_usuario == nombre_usuario)
                {
                    return usuario;
                }
            }
            return null;
        }

        private Rol SeleccionarRol(Rol rol_admin, Rol rol_cliente)
        {
            Console.WriteLine("Rol (1. admin, 2. cliente): ");
            int opcion_rol = LeerNumero("Elige rol: ");
            return opcion_rol == 1 ? rol_admin : rol_cliente;
        }

        private void HacerCompra(Inventario inventario, Carrito carrito)
        {
            if (inventario.total == 0)
            {
                Console.WriteLine("No hay productos disponibles.");
                return;
            }

            inventario_simple.MostrarInventarioBasico(inventario);
            int indice_producto = LeerNumero("Elige el numero del producto: ") - 1;
            if (indice_producto < 0 || indice_producto >= inventario.total)
            {
                Console.WriteLine("Producto no válido.");
                return;
            }

            int cantidad_compra = LeerNumero("Cantidad: ");
            Producto producto_elegido = inventario.ObtenerProducto(indice_producto);

            if (cantidad_compra <= producto_elegido.cantidad)
            {
                carrito.Agregar(producto_elegido, cantidad_compra);
                producto_elegido.cantidad -= cantidad_compra;
                Console.WriteLine("Compra realizada.");
                carrito.MostrarCarrito();
            }
            else
            {
                Console.WriteLine("No hay suficiente stock.");
            }
        }

        private int LeerNumero(string mensaje)
        {
            int valor_ingresado;
            Console.Write(mensaje);
            while (!int.TryParse(Console.ReadLine(), out valor_ingresado))
            {
                Console.Write("Entrada no válida. " + mensaje);
            }
            return valor_ingresado;
        }

        private double LeerDecimal(string mensaje)
        {
            double valor_ingresado;
            Console.Write(mensaje);
            while (!double.TryParse(Console.ReadLine(), out valor_ingresado))
            {
                Console.Write("Entrada no válida. " + mensaje);
            }
            return valor_ingresado;
        }

        private string LeerCadena(string mensaje)
        {
            Console.Write(mensaje);
            return Console.ReadLine();
        }
    }
}
