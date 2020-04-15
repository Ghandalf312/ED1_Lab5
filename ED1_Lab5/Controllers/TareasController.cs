using ED1_Lab5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ED1_Lab4.Controllers
{
    public class TareasController : Controller
    {
        public static List<TareaPendiente> CargaTareas = new List<TareaPendiente>();
        public static List<Usuario> IngresoUsuario = new List<Usuario>();

        // GET: Tareas/PaginaPrincipal
        public ActionResult PaginaPrincipal()
        {
            return View();
        }


        // GET: Tareas/Create
        public ActionResult Login()
        {
            return View();
        }

        // POST: Tareas/Create
        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            try
            {
                Usuario IngresoUser = new Usuario()
                {
                    User = collection["User"],
                    Contraseña = collection["Contraseña"],
                    ProyectManager = Convert.ToBoolean(collection["ProyectManager"])

                };
                IngresoUsuario.Add(IngresoUser);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        // GET: Tareas
        public ActionResult Index()
        {
            return View(CargaTareas);
        }

        // GET: Tareas/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //--------------CARGA DE DATOS------------------
        // GET: Tareas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tareas/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                TareaPendiente NuevoPendiente = new TareaPendiente()
                {
                    Titulo = collection["Titulo"],
                    Proyecto = collection["Proyecto"],
                    Descripcion = collection["Descripcion"],
                    Prioridad = Convert.ToInt16(collection["Prioridad"]),
                    FechaEntrega = collection["FechaEntrega"]

                };
                CargaTareas.Add(NuevoPendiente);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Tareas/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Tareas/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Tareas/Delete/5
        public ActionResult Delete(int id)
        {
            return View(CargaTareas);
        }

        // POST: Tareas/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                TareaPendiente EliminarPendiente = new TareaPendiente()
                {
                    Id = id,
                    Titulo = collection["Titulo"],
                    Proyecto = collection["Proyecto"],
                    Descripcion = collection["Descripcion"],
                    Prioridad = Convert.ToInt16(collection["Prioridad"]),
                    FechaEntrega = collection["FechaEntrega"]

                };
                CargaTareas.Remove(EliminarPendiente);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}