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
        public static List<Usuario> IngresoUsuario = new List<Usuario>();



        private string RutaTareas = AppDomain.CurrentDomain.BaseDirectory + "/tareas";

        //CREAR una Tabla Hash Global

        HashTable<string,TareaPendiente> HashTareas;

        //TareaPendiente
        TareaPendiente TareaRaiz;


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

                HashTareas.Insertar(NuevoPendiente.Titulo, NuevoPendiente);
                
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

                HashTareas.Eliminar(EliminarPendiente.Titulo);
                CargaTareas.Remove(EliminarPendiente);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        //GET
        public ActionResult CrearArch()
        {
            ViewBag.Message = "Creacion de archivo";
            return View();
        }
        //Post
        [HttpPost]
        public ActionResult CrearArch(HttpPostedFileBase postedFile)
        {
            StreamWriter Escribir = new StreamWriter(RutaTareas);
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
                            //Necesito Cargar Las tareas a la tabla hash global
                            //Que hay que enviar

                            //Esta es la variable la cual carga los datos del csv al programa 
                            TareaPendiente CargaArchTarea = HashTareas.Insertar<TareaRaiz.Titulo, tareaPendiente>;

                            CargaArchTarea.Titulo = infor_separada[1];
                            CargaArchTarea.Descripcion = infor_separada[2];
                            CargaArchTarea.Proyecto = infor_separada[3];
                            CargaArchTarea.Prioridad = Convert.ToInt32(infor_separada[4]);
                            CargaArchTarea.FechaEntrega = infor_separada[5];
                            
                            lector = archivolec.ReadLine();
                        }
                        lector = archivolec.ReadLine();

                    }
                    Raiz = LlamarBalanceo(Raiz);
                }
            }



            return RedirectToAction("PaginaPrincipal");
        }
    }

}