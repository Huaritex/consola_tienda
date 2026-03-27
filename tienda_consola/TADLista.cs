using System;

namespace tienda_consola
{
    public class Nodo<T>
    {
        public T Valor { get; set; }
        public Nodo<T> Siguiente { get; set; }
        public Nodo(T valor)
        {
            Valor = valor;
            Siguiente = null;
        }
    }

    public class TADLista<T>
    {
        private Nodo<T> cabeza;
        public int Cantidad { get; private set; }
        public TADLista()
        {
            cabeza = null;
            Cantidad = 0;
        }
        public void Agregar(T valor)
        {
            Nodo<T> nuevo = new Nodo<T>(valor);
            if (cabeza == null)
            {
                cabeza = nuevo;
            }
            else
            {
                Nodo<T> actual = cabeza;
                while (actual.Siguiente != null)
                {
                    actual = actual.Siguiente;
                }
                actual.Siguiente = nuevo;
            }
            Cantidad++;
        }
        public T Obtener(int indice)
        {
            if (indice < 0 || indice >= Cantidad)
            {
                throw new IndexOutOfRangeException("Índice fuera de los límites.");
            }
            
            Nodo<T> actual = cabeza;
            for (int i = 0; i < indice; i++)
            {
                actual = actual.Siguiente;
            }
            return actual.Valor;
        }
        public bool Contiene(T valor)
        {
            Nodo<T> actual = cabeza;
            while (actual != null)
            {
                if (actual.Valor != null && actual.Valor.Equals(valor))
                {
                    return true;
                }
                actual = actual.Siguiente;
            }
            return false;
        }

        public void EliminarEn(int indice)
        {
            if (indice < 0 || indice >= Cantidad)
            {
                throw new IndexOutOfRangeException("Índice fuera de los límites.");
            }

            if (indice == 0)
            {
                cabeza = cabeza.Siguiente;
                Cantidad--;
                return;
            }

            Nodo<T> actual = cabeza;
            for (int i = 0; i < indice - 1; i++)
            {
                actual = actual.Siguiente;
            }

            actual.Siguiente = actual.Siguiente != null ? actual.Siguiente.Siguiente : null;
            Cantidad--;
        }
    }
}