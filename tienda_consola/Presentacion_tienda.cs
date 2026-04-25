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
            Rol rol_vip = new Rol("cliente_vip", permisos_cliente);

            DescuentoVolumen desc_vol = new DescuentoVolumen("Descuento por Volumen", 500, 5);
            rol_cliente.AgregarDescuento(desc_vol);
            
            rol_vip.AgregarDescuento(new DescuentoFijo("Descuento VIP", 10));
            rol_vip.AgregarDescuento(desc_vol);

            Usuario usuario_admin = new Usuario("admin", "1234", rol_admin);
            Usuario usuario_vip = new Usuario("vip", "1234", rol_vip);

            TADLista<Usuario> lista_usuarios = new TADLista<Usuario>();
            lista_usuarios.Agregar(usuario_admin);
            lista_usuarios.Agregar(new Usuario("cliente", "1234", rol_cliente));
            lista_usuarios.Agregar(usuario_vip);

            Inventario inventario_principal = new Inventario();
            inventario_principal.AgregarProducto("PlayStation 5", 10, 500);
            inventario_principal.AgregarProducto("Xbox Series X", 8, 500);
            inventario_principal.AgregarProducto("Nintendo Switch", 15, 300);
            inventario_principal.AgregarProducto("Steam Deck", 5, 450);

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
                        Console.WriteLine("9. Gestionar Descuentos");
                        Console.WriteLine("10. Cerrar sesion");
                        Console.WriteLine("11. Cerrar tienda");
                        int opcion_admin = LeerNumero("Elige una opcion: ");

                        switch (opcion_admin)
                        {
                            case 1: MP_Inventario(inventario_principal); break;
                            case 2: RegistrarProducto(inventario_principal); break;
                            case 3: EditarProducto(inventario_principal); break;
                            case 4: QuitarProducto(inventario_principal); break;
                            case 5: MostrarUsuarios(lista_usuarios); break;
                            case 6: RegistrarUsuario(lista_usuarios, rol_admin, rol_cliente, rol_vip); break;
                            case 7: EditarUsuario(lista_usuarios, rol_admin, rol_cliente, rol_vip); break;
                            case 8: QuitarUsuario(lista_usuarios, usuario_en_sesion); break;
                            case 9: MenuGestionDescuentos(rol_cliente, rol_vip); break;
                            case 10: usuario_en_sesion.cerrar_Sesion(); sesion_cerrada = true; break;
                            case 11: tienda_cerrada = true; break;
                            default: Console.WriteLine("Opción no válida."); break;
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
                            case 1: MP_Inventario(inventario_principal); break;
                            case 2: HacerCompra(inventario_principal, carrito_compra, usuario_en_sesion); break;
                            case 3: usuario_en_sesion.cerrar_Sesion(); sesion_cerrada = true; break;
                            case 4: tienda_cerrada = true; break;
                            default: Console.WriteLine("Opción no válida."); break;
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
                    if (usuario.iniciar_Sesion(usuario_ingresado, clave_ingresada)) return usuario;
                }
                Console.WriteLine("Usuario o contraseña incorrectos.");
            }
        }

        private void RegistrarProducto(Inventario inventario)
        {
            string nombre = LeerCadena("Nombre del producto: ");
            int cant = LeerNumero("Cantidad: ");
            double precio = LeerDecimal("Precio: ");
            inventario.AgregarProducto(nombre, cant, precio);
            Console.WriteLine("Producto agregado.");
        }

        private void EditarProducto(Inventario inventario)
        {
            inventario_simple.MostrarInventarioBasico(inventario);
            int idx = LeerNumero("Numero del producto a actualizar: ") - 1;
            if (idx < 0 || idx >= inventario.total) return;
            Producto p = inventario.ObtenerProducto(idx);
            p.nombre = LeerCadena("Nuevo nombre: ");
            p.cantidad = LeerNumero("Nueva cantidad: ");
            p.precio = LeerDecimal("Nuevo precio: ");
            Console.WriteLine("Producto actualizado.");
        }

        private void QuitarProducto(Inventario inventario)
        {
            inventario_simple.MostrarInventarioBasico(inventario);
            int idx = LeerNumero("Numero del producto a eliminar: ") - 1;
            if (idx < 0 || idx >= inventario.total) return;
            inventario.EliminarProducto(idx);
            Console.WriteLine("Producto eliminado.");
        }

        private void MostrarUsuarios(TADLista<Usuario> usuarios)
        {
            Console.WriteLine("\nUsuarios:");
            for (int i = 0; i < usuarios.Cantidad; i++)
            {
                Usuario u = usuarios.Obtener(i);
                Console.WriteLine($"{i + 1}. {u.name_usuario} | Rol: {u.rol.ObtenerTipo()}");
            }
        }

        private void RegistrarUsuario(TADLista<Usuario> usuarios, Rol r_admin, Rol r_cliente, Rol r_vip)
        {
            string nom = LeerCadena("Nombre de usuario: ");
            string pwd = LeerCadena("Contraseña: ");
            Rol r = SeleccionarRol(r_admin, r_cliente, r_vip);
            usuarios.Agregar(new Usuario(nom, pwd, r));
            Console.WriteLine("Usuario agregado.");
        }

        private void EditarUsuario(TADLista<Usuario> usuarios, Rol r_admin, Rol r_cliente, Rol r_vip)
        {
            string nom = LeerCadena("Usuario a actualizar: ");
            Usuario u = BuscarUsuario(usuarios, nom);
            if (u == null) return;
            u.pwd = LeerCadena("Nueva contraseña: ");
            u.rol = SeleccionarRol(r_admin, r_cliente, r_vip);
            Console.WriteLine("Usuario actualizado.");
        }

        private void QuitarUsuario(TADLista<Usuario> usuarios, Usuario sesion)
        {
            string nom = LeerCadena("Usuario a eliminar: ");
            Usuario u = BuscarUsuario(usuarios, nom);
            if (u == null || u == sesion) return;
            for (int i = 0; i < usuarios.Cantidad; i++)
                if (usuarios.Obtener(i) == u) { usuarios.EliminarEn(i); break; }
            Console.WriteLine("Usuario eliminado.");
        }

        private Usuario BuscarUsuario(TADLista<Usuario> lista, string nom)
        {
            for (int i = 0; i < lista.Cantidad; i++)
                if (lista.Obtener(i).name_usuario == nom) return lista.Obtener(i);
            return null;
        }

        private Rol SeleccionarRol(Rol r_admin, Rol r_cliente, Rol r_vip)
        {
            Console.WriteLine("Rol (1. admin, 2. cliente, 3. vip): ");
            int op = LeerNumero("Elige: ");
            if (op == 1) return r_admin;
            if (op == 3) return r_vip;
            return r_cliente;
        }

        private void MenuGestionDescuentos(Rol cliente, Rol vip)
        {
            Console.WriteLine("\n GESTION DE DESCUENTOS ");
            Console.WriteLine("1. Modificar Porcentaje VIP");
            Console.WriteLine("2. Modificar Descuento Volumen (Monto y %)");
            Console.WriteLine("3. Volver");
            int opt = LeerNumero("Opcion: ");

            if (opt == 1)
            {
                for (int i = 0; i < vip.descuentos.Cantidad; i++)
                    if (vip.descuentos.Obtener(i) is DescuentoFijo df)
                        df.Porcentaje = LeerDecimal($"Nuevo % VIP (actual {df.Porcentaje}%): ");
            }
            else if (opt == 2)
            {
                double m = LeerDecimal("Nuevo monto minimo: ");
                double p = LeerDecimal("Nuevo %: ");
                ActualizarDescuentoVolumen(cliente, m, p);
                ActualizarDescuentoVolumen(vip, m, p);
            }
        }

        private void ActualizarDescuentoVolumen(Rol rol, double monto, double porc)
        {
            for (int i = 0; i < rol.descuentos.Cantidad; i++)
                if (rol.descuentos.Obtener(i) is DescuentoVolumen dv)
                { dv.MontoMinimo = monto; dv.Porcentaje = porc; }
        }

        private void HacerCompra(Inventario inventario, Carrito carrito, Usuario usuario)
        {
            inventario_simple.MostrarInventarioBasico(inventario);
            int idx = LeerNumero("Producto #: ") - 1;
            if (idx < 0 || idx >= inventario.total) return;

            int cant = LeerNumero("Cantidad: ");
            Producto p = inventario.ObtenerProducto(idx);

            if (cant <= p.cantidad)
            {
                double subtotal = p.precio * cant;
                double totalDescuento = usuario.rol.CalcularTotalDescuentos(subtotal);
                double totalFinal = subtotal - totalDescuento;

                Console.WriteLine($"\n RESUMEN ");
                Console.WriteLine($"Subtotal: {subtotal:F2} bs");
                for (int i = 0; i < usuario.rol.descuentos.Cantidad; i++)
                {
                    var d = usuario.rol.descuentos.Obtener(i);
                    double desc = d.Calcular(subtotal);
                    if (desc > 0) Console.WriteLine($"{d.Nombre}: -{desc:F2} bs");
                }
                Console.WriteLine($"TOTAL A PAGAR: {totalFinal:F2} bs");

                if (LeerCadena("¿Confirmar compra? (s/n): ").ToLower() == "s")
                {
                    p.cantidad -= cant;
                    carrito.Agregar(p, cant);
                    Console.WriteLine("\nFACTURA ");
                    Console.WriteLine($"Producto: {p.nombre} x{cant}");
                    Console.WriteLine($"Total Pagado: {totalFinal:F2} bs");
                }
            }
            else Console.WriteLine("Stock insuficiente.");
        }

        private int LeerNumero(string m) { Console.Write(m); int.TryParse(Console.ReadLine(), out int v); return v; }
        private double LeerDecimal(string m) { Console.Write(m); double.TryParse(Console.ReadLine(), out double v); return v; }
        private string LeerCadena(string m) { Console.Write(m); return Console.ReadLine() ?? ""; }
    }
}
