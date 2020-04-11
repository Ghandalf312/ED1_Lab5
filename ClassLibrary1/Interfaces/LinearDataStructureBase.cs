using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary1.Interfaces
{
    public abstract class LinearDataStructureBase<T>
    {
        protected abstract void Insertar(T valor, int prioridad);
        protected abstract void Eliminar();
        protected abstract T Get();
    }
}
