public class Inventario
{
    public Producto[] productos;
    public int total;

    public Inventario()
    {
        productos = new Producto[100]; 
        total = 0;
    }

    public void AgregarProducto(string nombre, int cantidad, double precio)
    {
        productos[total] = new Producto(nombre, cantidad, precio);
        total++;
    }

    public void MostrarProductos()
    {
        Console.WriteLine("\nProductos disponibles");
        for (int i = 0; i < total; i++)
        {
            Console.WriteLine((i + 1) + ". " + productos[i].nombre +
                "Cantidad: " + productos[i].cantidad +
                "Precio: " + productos[i].precio);
        }
    }
}