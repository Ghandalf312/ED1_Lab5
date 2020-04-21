using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary1.Structures
{
    class Cola<T>
    {
        private Nodo<T> Primero { get; set; }

        public void Insertar(T valor, int prioridad)
        {
            if (Primero == null)
            {
                Primero = NuevoNodo(valor, prioridad);
            }
            Nodo<T> inicio = Primero;

            Nodo<T> temp = NuevoNodo(valor, prioridad);

            if (Primero.prioridad > prioridad)
            {
                temp.siguiente = Primero;
                Primero = temp;
            }
            else
            {
                while (inicio.siguiente != null && inicio.siguiente.prioridad < prioridad)
                {
                    inicio = inicio.siguiente;
                }
            }
            temp.siguiente = inicio.siguiente;
            inicio.siguiente = temp;
        }
        public T Eliminar()
        {
            var value = Peek();
            EliminarColaPrioridad();
            return value;
        }
        protected void EliminarColaPrioridad()
        {
            if (Primero != null)
            {
                Primero = Primero.siguiente;
            }
        }
        protected T Peek()
        {
            return Primero.valor;
        }

        public static Nodo<T> NuevoNodo(T valor, int prioridad)
        {
            var temp = new Nodo<T>();
            temp.valor = valor;
            temp.prioridad = prioridad;
            temp.siguiente = null;

            return temp;
        }
    }
}
