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
    public class DevolucionesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Devoluciones
        public ActionResult Index()
        {
            var devolucion = db.Devolucion.Include(d => d.Prestamo);
            return View(devolucion.ToList());
        }

        // GET: Devoluciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Devolucion devolucion = db.Devolucion.Find(id);
            if (devolucion == null)
            {
                return HttpNotFound();
            }
            return View(devolucion);
        }

        // GET: Devoluciones/Create
        public ActionResult Create()
        {
            ViewBag.id_prestamo = new SelectList(db.Prestamos, "id_prestamo", "id_prestamo");
            return View();
        }

        // POST: Devoluciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_devolucion,id_prestamo,hora,anulada")] Devolucion devolucion)
        {
            if (ModelState.IsValid)
            {
                db.Devolucion.Add(devolucion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_prestamo = new SelectList(db.Prestamos, "id_prestamo", "id_prestamo", devolucion.id_prestamo);
            return View(devolucion);
        }

        // GET: Devoluciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Devolucion devolucion = db.Devolucion.Find(id);
            if (devolucion == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_prestamo = new SelectList(db.Prestamos, "id_prestamo", "id_prestamo", devolucion.id_prestamo);
            return View(devolucion);
        }

        // POST: Devoluciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_devolucion,id_prestamo,hora,anulada")] Devolucion devolucion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(devolucion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_prestamo = new SelectList(db.Prestamos, "id_prestamo", "id_prestamo", devolucion.id_prestamo);
            return View(devolucion);
        }

        // GET: Devoluciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Devolucion devolucion = db.Devolucion.Find(id);
            if (devolucion == null)
            {
                return HttpNotFound();
            }
            return View(devolucion);
        }

        // POST: Devoluciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Devolucion devolucion = db.Devolucion.Find(id);
            db.Devolucion.Remove(devolucion);
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
