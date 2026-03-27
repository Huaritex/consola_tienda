namespace tienda_consola
{
    public class Rol
    {
        private string tipo;
        private TADLista<string> permisos;

        public Rol(string tipo, TADLista<string> permisos)
        {
            this.tipo = tipo;
            this.permisos = permisos != null ? permisos : new TADLista<string>();
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
    }
}
