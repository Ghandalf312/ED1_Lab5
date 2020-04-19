using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary1.Interfaces
{
    interface ILinearDataStructure<T>
    {
        void Insertar();
        void Eliminar();
        T Get();
    }
}
