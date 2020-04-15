using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ED1_Lab5.Models
{
    public class Usuario
    {
        public string User { get; set; }
        public string Contraseña { get; set; }
        public bool ProyectManager { get; set; }

        public List<TareaPendiente> TareasUsuario = new List<TareaPendiente>();
    }
}