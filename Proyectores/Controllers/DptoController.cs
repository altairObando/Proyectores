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
    public class DptoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Dpto
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult getDepartamentos()
        {
            List<Departamento> lista = db.Departamentos.ToList();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Create(int id = 0)
        {
            if (id == 0)
                return View(new Departamento());
            else
                return View(db.Departamentos.FirstOrDefault(x=> x.id_departamento == id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include ="id_departamento,nombre,mision,vision,historia,ubicacion,organizacion")]Departamento departamento)
        {
            if(departamento.id_departamento == 0)
            {
                db.Departamentos.Add(departamento);
                db.SaveChanges();
            }else
            {
                db.Entry(departamento).State = EntityState.Modified;
                db.SaveChanges();
            }
            return Json(new {success= true, message="Operacion finalizada" }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            
                Departamento emp = db.Departamentos.Where(x => x.id_departamento == id).FirstOrDefault<Departamento>();
                db.Departamentos.Remove(emp);
                db.SaveChanges();
                return Json(new { success = true, message = "Borrado Completamente" }, JsonRequestBehavior.AllowGet);
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
