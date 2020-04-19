using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary1.Structures
{
    class Nodo<T>
    {
        public Nodo<T> siguiente { get; set; }
        public T valor { get; set; }
        public int prioridad { get; set; }
    }
}
