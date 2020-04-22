using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ED1_Lab5.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        public string User { get; set; }
        public List<TareaPendiente> tareaPendientes = new List<TareaPendiente>();

    }
}