using tienda_consola;

public class Inventario
{
    public TADLista<Producto> productos;

    public int total => productos.Cantidad;

    public Inventario()
    {
        productos = new TADLista<Producto>(); 
    }

    public void AgregarProducto(string nombre, int cantidad, double precio)
    {
        productos.Agregar(new Producto(nombre, cantidad, precio));
    }

    public Producto ObtenerProducto(int indice)
    {
        return productos.Obtener(indice);
    }

    public void EliminarProducto(int indice)
    {
        productos.EliminarEn(indice);
    }
}