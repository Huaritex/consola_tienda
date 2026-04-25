namespace tienda_consola
{
    public class Rol
    {
        private string tipo;
        private TADLista<string> permisos;
        public TADLista<Descuento> descuentos;

        public Rol(string tipo, TADLista<string> permisos)
        {
            this.tipo = tipo;
            this.permisos = permisos != null ? permisos : new TADLista<string>();
            this.descuentos = new TADLista<Descuento>();
        }

        public string ObtenerTipo() 
        {
            return this.tipo;
        }

        public bool validar_Permiso(string permiso)
        {
            return permisos.Contiene(permiso);
        }

        public bool es_admin()
        {
            return tipo.ToLower() == "admin";
        }
        
        public bool es_vip()
        {
            return tipo.ToLower() == "cliente_vip" || tipo.ToLower() == "vip";
        }

        public void AgregarDescuento(Descuento d)
        {
            descuentos.Agregar(d);
        }

        public double CalcularTotalDescuentos(double subtotal)
        {
            double total = 0;
            for (int i = 0; i < descuentos.Cantidad; i++)
            {
                total += descuentos.Obtener(i).Calcular(subtotal);
            }
            return total;
        }
    }
}

