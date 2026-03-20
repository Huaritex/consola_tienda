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
}