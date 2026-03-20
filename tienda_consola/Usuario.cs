using System;

namespace tienda_consola
{
    public class Usuario
    {
        public string name_usuario { get; set; }
        public string pwd { get; set; }

        public Usuario(string name_usuario, string pwd)
        {
            this.name_usuario = name_usuario;
            this.pwd = pwd;
        }

        public bool iniciar_Sesion(string input_user, string input_pwd)
        {
            return this.name_usuario == input_user && this.pwd == input_pwd;
        }

        public void cerrar_Sesion()
        {
            Console.WriteLine("Sesión cerrada.");
        }
    }
}
