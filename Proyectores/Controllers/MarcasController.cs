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
    public class MarcasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Marcas
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult getMarcas()
        {
            List<Marca> lista = db.Marca.ToList();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }   
        
        [HttpGet]
        public ActionResult Crear(int id = 0)
        {
            if (id == 0)
                return View(new Marca());
            else
            {
                var marca = db.Marca.FirstOrDefault(x => x.id_marca == id);
                return Json(new {success = true, id_marca = marca.id_marca, marca = marca.marca }, JsonRequestBehavior.AllowGet);
            }
        }    
        [HttpPost]
        public ActionResult Crear([Bind(Include ="id_marca, marca")]Marca marca)
        {
            int id = int.Parse(Request.Form["id_marca"]);
            string m = Request.Form["marca"];
            marca = new Marca { id_marca = id, marca = m };
            if (marca.id_marca == 0)
            {
                db.Marca.Add(marca);
                db.SaveChanges();
                return Json(new { success = true, message = "Marca Registrada Correctamente" }, JsonRequestBehavior.AllowGet);
            }else
            {
                db.Entry(marca).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, message = "Marca Actualizada Correctamente" }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            int result = 0;
            var m = db.Marca.FirstOrDefault(x => x.id_marca == id);
            db.Marca.Remove(m);
            result = db.SaveChanges();
            if(result > 0)
                return Json(new { success = true, message = "Marca Eliminada Correctamente" }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { success = false, message = "No se ha eliminado" }, JsonRequestBehavior.AllowGet);
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
