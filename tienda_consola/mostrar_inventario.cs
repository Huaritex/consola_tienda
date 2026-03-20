public class mostrar_inventario
{
    public void MostrarInventario(Inventario inventario)
    {
        Console.WriteLine("\nProductos disponibles:");
        for (int i = 0; i < inventario.total; i++)
        {
            Producto p = inventario.productos[i];
            Console.WriteLine((i + 1) + ". " + p.nombre + " - Cantidad: " + p.cantidad + " - Precio: " + p.precio);
        }
    }
}