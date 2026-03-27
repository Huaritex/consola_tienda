public class mostrar_inventario
{
    public void MostrarInventario(Inventario inventario)
    {
        Console.WriteLine("\nProductos disponibles:");
        for (int i = 0; i < inventario.total; i++)
        {
            Producto p = inventario.productos.Obtener(i);
            Console.WriteLine((i + 1) + ". " + p.nombre + " - Cantidad: " + p.cantidad + " - Precio: " + p.precio);
        }
    }

    public void MostrarInventarioBasico(Inventario inventario)
    {
        Console.WriteLine("\nProductos:");
        for (int i = 0; i < inventario.total; i++)
        {
            Producto producto = inventario.ObtenerProducto(i);
            Console.WriteLine((i + 1) + ". " + producto.nombre + " | Stock: " + producto.cantidad + " | Precio: " + producto.precio);
        }
    }
}