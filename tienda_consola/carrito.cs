public class Carrito
{
    public Producto[] items;
    public int total;

    public Carrito()
    {
        items = new Producto[50];
        total = 0;
    }

    public void Agregar(Producto producto, int cantidad)
    {
        items[total] = new Producto(producto.nombre, cantidad, producto.precio);
        total++;
        Console.WriteLine("Se agregó " + cantidad + " " + producto.nombre + " al carrito.");
    }

    public void MostrarCarrito()
    {
        if (total == 0)
        {
            Console.WriteLine("\nEl carrito está vacío.");
            return;
        }

        Console.WriteLine("\n--- Tu carrito ---");
        double totalPagar = 0;
        for (int i = 0; i < total; i++)
        {
            double subtotal = items[i].precio * items[i].cantidad;
            Console.WriteLine(items[i].nombre + " x" + items[i].cantidad + " = $" + subtotal);
            totalPagar += subtotal;
        }
        Console.WriteLine("Total a pagar: $" + totalPagar);
    }
}