using ClassLibrary1.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary1.Structures
{
    class Cola<T> : LinearDataStructureBase<T>
    {

        private Nodo<T> primero { get; set; }
        private Nodo<T> primeroAux { get; set; }
        public void Encolar(T valor, int prioridad)
        {
            Insertar(valor, prioridad);
            OrdenarColaPrioridad();
        }
        public T Desencolar()
        {
            var valor = Get();
            Eliminar();
            return valor;
        }
        protected override void Insertar(T valor, int prioridad)
        {
            if (primero == null)
            {
                primero = new Nodo<T>
                {
                    valor = valor,
                    siguiente = null,
                    prioridad = prioridad
                };
            }
            else
            {
                var actual = primero;
                while (actual.siguiente != null)
                {
                    actual = actual.siguiente;
                }
                actual.siguiente = new Nodo<T>
                {
                    valor = valor,
                    siguiente = null
                };
            }
        }
        public void InsertarAuxiliar(T valor, int prioridad)
        {
            if (primeroAux == null)
            {
                primeroAux = new Nodo<T>
                {
                    valor = valor,
                    siguiente = null,
                    prioridad = prioridad
                };
            }
            else
            {
                var actual = primeroAux;
                while (actual.siguiente != null)
                {
                    actual = actual.siguiente;
                }
                actual.siguiente = new Nodo<T>
                {
                    valor = valor,
                    siguiente = null
                };
            }
        }
        protected override void Eliminar()
        {
            if (primero != null)
            {
                primero = primero.siguiente;
            }
        }
        protected override T Get()
        {
            return primero.valor;
        }

        public void OrdenarColaPrioridad()
        {
            var aux = primero;
            for (int i = 1; i < 11; i++)//Va a meter los datos a una nueva cola por prioridad
            {
                while (aux != null)//Recorrera la cola buscando los valores con la prioridad del contador
                {
                    if (primero.prioridad == i)
                    {
                        InsertarAuxiliar(aux.valor, aux.prioridad);
                    }
                }
            }
            primero = primeroAux;
            primeroAux = null;
        }
    }
}
