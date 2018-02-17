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
    public class DepartamentosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Departamentos
        public ActionResult Index()
        {
            return View(db.Departamentos.ToList());
        }
        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new Departamento());
            else
            {
                
               return View(db.Departamentos.Where(x => x.id_departamento == id).FirstOrDefault<Departamento>());
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(Departamento emp)
        {
           if (emp.id_departamento == 0)
                {
                    db.Departamentos.Add(emp);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    db.Entry(emp).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
                }
           
        }
        public ActionResult getDeptos()
        {
            List<Departamento> lista = db.Departamentos.ToList();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Borrar(int id)
        {
            Departamento d = db.Departamentos.Where(x => x.id_departamento == id).FirstOrDefault();
            db.Departamentos.Remove(d);
            db.SaveChanges();
            return Json(new { success = true, message = "Borrado Completamente" }, JsonRequestBehavior.AllowGet);
        }
        // GET: Departamentos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Departamento departamento = db.Departamentos.Find(id);
            if (departamento == null)
            {
                return HttpNotFound();
            }
            return View(departamento);
        }

        // GET: Departamentos/Create
        [HttpGet]
        public ActionResult Create(int id=0)
        {
            if (id == 0)
                return View(new Departamento());
            else
                return View(db.Departamentos.Where(x => x.id_departamento == id).FirstOrDefault<Departamento>());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_departamento,nombre,mision,vision,historia,ubicacion,organizacion")] Departamento departamento)
        {
            if (departamento.id_departamento == 0)
            {
                db.Departamentos.Add(departamento);
                db.SaveChanges();
                return Json(new { succes = true, message = "Agregado Correctamente" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                db.Entry(departamento).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { succes = true, message = "Actualizado Correctamente" }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Departamentos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Departamento departamento = db.Departamentos.Find(id);
            if (departamento == null)
            {
                return HttpNotFound();
            }
            return View(departamento);
        }

        // POST: Departamentos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_departamento,nombre,mision,vision,historia,ubicacion,organizacion")] Departamento departamento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(departamento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(departamento);
        }

        // GET: Departamentos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Departamento departamento = db.Departamentos.Find(id);
            if (departamento == null)
            {
                return HttpNotFound();
            }
            return View(departamento);
        }

        // POST: Departamentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Departamento departamento = db.Departamentos.Find(id);
            db.Departamentos.Remove(departamento);
            db.SaveChanges();
            return RedirectToAction("Index");
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
