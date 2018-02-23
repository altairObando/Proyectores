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
    public class DocentesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Docentes
        public ActionResult Index()
        {
            
            return View();
        }
        [HttpGet]
        public ActionResult getDocentes()
        {
            List<Docente> lista = db.Docentes.Include(d => d.Departamento).Include(d => d.Especialidad).ToList();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }
        // GET: Docentes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Docente docente = db.Docentes.Find(id);
            if (docente == null)
            {
                return HttpNotFound();
            }
            return View(docente);
        }

        // GET: Docentes/Create
        public ActionResult Create(int id=0)
        {
            ViewBag.id_departamento = new SelectList(db.Departamentos, "id_departamento", "nombre");
            ViewBag.id_especialidad = new SelectList(db.Especialidad, "id_especialidad", "nombre");
            if (id == 0)
                return View(new Docente());
            else
            {
                return View(db.Docentes.FirstOrDefault(x=> x.DocenteId == id));
            }
        }

        // POST: Docentes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DocenteId,Nombre,Apellido,Telefono,Email,id_especialidad,id_departamento")] Docente docente)
        {
            if (ModelState.IsValid)
            {
                if(docente.DocenteId == 0)
                {
                    db.Docentes.Add(docente);
                    db.SaveChanges();
                    return Json(new { success = true, message ="Creado Correctamente"}, JsonRequestBehavior.AllowGet);
                }else
                {
                    db.Entry(docente).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Actualizado Correctamente" }, JsonRequestBehavior.AllowGet);
                }
            }
            ViewBag.id_departamento = new SelectList(db.Departamentos, "id_departamento", "nombre", docente.id_departamento);
            ViewBag.id_especialidad = new SelectList(db.Especialidad, "id_especialidad", "nombre", docente.id_especialidad);
            return View(docente);
        }

        // GET: Docentes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Docente docente = db.Docentes.Find(id);
            if (docente == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_departamento = new SelectList(db.Departamentos, "id_departamento", "nombre", docente.id_departamento);
            ViewBag.id_especialidad = new SelectList(db.Especialidad, "id_especialidad", "nombre", docente.id_especialidad);
            return View(docente);
        }

        // POST: Docentes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DocenteId,Nombre,Apellido,Telefono,Email,id_especialidad,id_departamento")] Docente docente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(docente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_departamento = new SelectList(db.Departamentos, "id_departamento", "nombre", docente.id_departamento);
            ViewBag.id_especialidad = new SelectList(db.Especialidad, "id_especialidad", "nombre", docente.id_especialidad);
            return View(docente);
        }

        // GET: Docentes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Docente docente = db.Docentes.Find(id);
            if (docente == null)
            {
                return HttpNotFound();
            }
            return View(docente);
        }

        // POST: Docentes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Docente docente = db.Docentes.Find(id);
            db.Docentes.Remove(docente);
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
