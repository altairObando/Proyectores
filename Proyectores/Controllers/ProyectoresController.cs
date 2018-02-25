using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Proyectores.Models;

namespace Proyectores.Controllers
{
    public class ProyectoresController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Proyectors
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult getProyectores()
        {
            var lista = from item in db.Proyectores
                        where item.activo == true
                        select new {
                            marca = item.Marca.marca,
                            nombre = item.nombre,
                            estado = item.Estado.nombre,
                            id_proyector = item.id_proyector
                        };
            return Json(new {data = lista}, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Crear(int id = 0)
        {
            ViewBag.lista_marcas = new SelectList(db.Marca.ToList(), "id_marca", "marca");
            ViewBag.lista_estados = new SelectList(db.Estados.ToList(), "id_estado", "nombre");            
            if (id == 0)
                return View(new Proyector());
            else
                return View(db.Proyectores.FirstOrDefault(x => x.id_proyector == id));
        }
        [HttpPost]
        public ActionResult Crear([Bind(Include = "id_proyector,lista_marcas,lista_estados,nombre,activo")]Proyector proyector)
        {
            bool band = false;
            string msj = "";
            try
            {
                int id_marca = int.Parse(Request.Form["lista_marcas"]);
                int id_estado = int.Parse(Request.Form["lista_estados"]);

                if (id_marca > 0 && id_estado > 0)
                {
                    proyector.id_marca = id_marca;
                    proyector.id_estado = id_estado;

                    if (proyector.id_proyector == 0)
                    {
                        if (ModelState.IsValid)
                        {
                            db.Proyectores.Add(proyector);
                            db.SaveChanges();
                            band = true;
                            msj = "Proyector registrado Correctamente";
                        }
                    }
                    else
                    {
                        db.Entry(proyector).State = EntityState.Modified;
                        db.SaveChanges();
                        band = true;
                        msj = "Actualizado correctamente";
                    }
                }
                else
                {
                    throw new Exception("No se ha seleccionado Marca ó Estado de Proyector");
                }
            }
            catch (Exception ex)
            {
                band = false;
                msj = "Error: " + ex.Message;
            }
            return Json(new { success = band, message = msj }, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
