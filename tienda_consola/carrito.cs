using tienda_consola;

public class Carrito
{
    public TADLista<Producto> items;
    
    public int total => items.Cantidad;

    public Carrito()
    {
        items = new TADLista<Producto>();
    }

    public void Agregar(Producto producto, int cantidad)
    {
        items.Agregar(new Producto(producto.nombre, cantidad, producto.precio));
    }

    public void MostrarCarrito()
    {
        if (total == 0)
        {
            Console.WriteLine("\nEl carrito está vacío.");
            return;
        }

        Console.WriteLine("\nTu carrito");
        double totalPagar = 0;
        for (int i = 0; i < total; i++)
        {
            Producto p = items.Obtener(i);
            double subtotal = p.precio * p.cantidad;
            Console.WriteLine(p.nombre + " x" + p.cantidad + " = " + subtotal);
            totalPagar += subtotal;
        }
        Console.WriteLine("Total a pagar: " + totalPagar);
    }
}