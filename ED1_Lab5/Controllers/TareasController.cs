using ED1_Lab5.Models;
using ClassLibrary1.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Text.RegularExpressions;

namespace ED1_Lab4.Controllers
{
    public class TareasController : Controller
    {
        public static List<TareaPendiente> CargaTareas = new List<TareaPendiente>();
        public static List<TareaPendiente> CargaTareasGlobal = new List<TareaPendiente>();
        public static List<Usuario> IngresoUsuario = new List<Usuario>();


        //public override int GetHashCode()
        //{
        //    return base.GetHashCode();
        //}

        private string RutaTareas = AppDomain.CurrentDomain.BaseDirectory + "/Tareas.csv";

        //CREAR una Tabla Hash Global
        public HashTable<string,TareaPendiente> HashTareas;

         Cola<string> ColaPrioridad;

        //TareaPendiente
        public TareaPendiente TareaRaiz;

        StreamWriter Escribir;

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
                    
                };
                
                IngresoUsuario.Add(IngresoUser);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        //VISTA MANAGER
        // GET: Tareas
        public ActionResult IndexManager()
        {
            CargaTareasGlobal.Sort((x, y) => x.Prioridad.CompareTo(y.Prioridad));
            return View(CargaTareasGlobal);
        }

        //VISTA DEVELOPER
        //ORDENAR POR PRIORIDAD
        // GET: Tareas
        public ActionResult Index()
        {
            IngresoUsuario[IngresoUsuario.Count - 1].tareaPendientes.Sort((x, y) => x.Prioridad.CompareTo(y.Prioridad));

            //return View(IngresoUsuario[IngresoUsuario.Count-1]);
            return View(IngresoUsuario[IngresoUsuario.Count - 1].tareaPendientes);
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
                   User =IngresoUsuario[IngresoUsuario.Count-1].User,
                    Titulo = collection["Titulo"],
                    Proyecto = collection["Proyecto"],
                    Descripcion = collection["Descripcion"],
                    Prioridad = Convert.ToInt16(collection["Prioridad"]),
                    FechaEntrega = collection["FechaEntrega"]

                };


                //Listas
                IngresoUsuario[IngresoUsuario.Count - 1].tareaPendientes.Add(NuevoPendiente);
                CargaTareas.Add(NuevoPendiente);
                CargaTareasGlobal.Add(NuevoPendiente);

                //Estructuras
                HashTareas.Insertar(NuevoPendiente.Titulo, NuevoPendiente);
                ColaPrioridad.Insertar(NuevoPendiente.Titulo, NuevoPendiente.Prioridad);


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //public ActionResult PrioridadTareas()
        //{

        //    return View(CargaTareas[0]);
        //}

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
        public ActionResult PrioridadTareas(int id)
        {
            try
            {
                return View(IngresoUsuario[IngresoUsuario.Count - 1].tareaPendientes[0]);
            }
            catch 
            {
                return RedirectToAction("PaginaPrincipal");
            }
            
        }

        // POST: Tareas/Delete/5
        [HttpPost]
        public ActionResult PrioridadTareas(int id, FormCollection collection)
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
                IngresoUsuario[IngresoUsuario.Count - 1].tareaPendientes.Remove(IngresoUsuario[IngresoUsuario.Count - 1].tareaPendientes[0]);
                CargaTareas.Remove(CargaTareas[0]);
                //HashTareas.Eliminar(EliminarPendiente.Titulo);

                return RedirectToAction("Index");

            }
            catch
            {
                return View();
            }
        }

        public ActionResult CrearArch()
        {
            ViewBag.Message = "Elección de archivo";
            return View();
        }
        //Post
        [HttpPost]
        public ActionResult Crear(HttpPostedFileBase postedFile)
        {
            Escribir = new StreamWriter(RutaTareas);
            string Contenido = null;

            for (int i = 0;i < CargaTareas.Count; i++)
            {
                Contenido = string.Format("{0},{1},{2},{3},{4}", CargaTareas[i].Id, CargaTareas[i].Titulo, CargaTareas[i].Proyecto, CargaTareas[i].Descripcion, CargaTareas[i].Prioridad, CargaTareas[i].FechaEntrega);
                Escribir.WriteLine(Contenido);
            }
            Escribir.Close();
            return RedirectToAction("Index");
        }
        //Carga de Archivo
        //GET
        public ActionResult CargaArch()
        {
            ViewBag.Message = "Elección de archivo";
            return View();
        }
        //Post
        [HttpPost]
        public ActionResult Carga(HttpPostedFileBase postedFile)
        {

            string directarchivo = string.Empty;
            if (postedFile != null)
            {
                string path = Server.MapPath("~/Cargas/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                directarchivo = path + Path.GetFileName(postedFile.FileName);
                postedFile.SaveAs(directarchivo);
                //Caja_arbol.Instance.direccion_archivo_arbol = directarchivo;
            }
            //Modificación de los digitos de la exitencia
            using (var archivo = new FileStream(directarchivo, FileMode.Open))
            {
                using (var archivolec = new StreamReader(archivo))
                {
                    string lector = archivolec.ReadLine();
                    lector = archivolec.ReadLine();
                    while (lector != null)
                    {
                        Regex regx = new Regex("," + "(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
                        string[] infor_separada = regx.Split(lector);
                        if (infor_separada[infor_separada.Length - 1].Length < 2)
                        {
                            infor_separada[infor_separada.Length - 1] = "0" + infor_separada[infor_separada.Length - 1];

                        }
                        if (infor_separada.Length == 6)
                        {

                            TareaPendiente CargaArchTarea = new TareaPendiente();

                            CargaArchTarea.Titulo = infor_separada[1];
                            CargaArchTarea.Descripcion = infor_separada[2];
                            CargaArchTarea.Proyecto = infor_separada[3];
                            CargaArchTarea.Prioridad = Convert.ToInt32(infor_separada[4]);
                            CargaArchTarea.FechaEntrega = infor_separada[5];

                            HashTareas.Insertar(CargaArchTarea.Titulo, CargaArchTarea);

                            lector = archivolec.ReadLine();
                        }
                        lector = archivolec.ReadLine();

                    }
                }
            }



            return RedirectToAction("PaginaPrincipal");
        }
    }

}