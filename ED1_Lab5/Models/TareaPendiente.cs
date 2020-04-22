using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ED1_Lab5.Models
{
    public class TareaPendiente
    {
        public int Id { get; set; }
        public string User { get; set; }

        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Proyecto { get; set; }
        public int Prioridad { get; set; }
        public string FechaEntrega { get; set; }

    }
}