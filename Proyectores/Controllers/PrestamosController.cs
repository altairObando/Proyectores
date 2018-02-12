using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Proyectores.Models;
using Microsoft.AspNet.Identity;

namespace Proyectores.Controllers
{
    public class PrestamosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Prestamos
        public ActionResult Index()
        {
            var prestamos = db.Prestamos
                            .Include(p => p.Docente)
                            .Include(p => p.Proyector)
                            ;
            return View(prestamos.ToList());
        }

        // GET: Prestamos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prestamo prestamo = db.Prestamos.Find(id);
            if (prestamo == null)
            {
                return HttpNotFound();
            }
            return View(prestamo);
        }

        // GET: Prestamos/Create
        public ActionResult Create()
        {
            ViewBag.id_proyector = new SelectList(db.Proyectores, "id_proyector", "nombre");
            ViewBag.id_docente = new SelectList(db.Docentes, "DocenteId", "Nombre" );
            return View();
        }

        // POST: Prestamos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_prestamo,id_proyector,id_docente,id_responsable,fecha,prestamo_anulado,finalizado,activo")] Prestamo prestamo)
        {
            var userID = User.Identity.GetUserId();
            prestamo.id_responsable = userID;
            if (ModelState.IsValid)
            {
                prestamo.Docente = db.Docentes.FirstOrDefault(x => x.DocenteId == prestamo.id_docente);
                prestamo.Responsable = db.Users.FirstOrDefault(x => x.Id == prestamo.id_responsable);
                db.Prestamos.Add(prestamo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_proyector = new SelectList(db.Proyectores, "id_proyector", "nombre", prestamo.id_proyector);
            return View(prestamo);
        }

        // GET: Prestamos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prestamo prestamo = db.Prestamos.Find(id);
            if (prestamo == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_proyector = new SelectList(db.Proyectores, "id_proyector", "nombre", prestamo.id_proyector);
            return View(prestamo);
        }

        // POST: Prestamos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_prestamo,id_proyector,id_docente,id_responsable,fecha,prestamo_anulado,finalizado,activo")] Prestamo prestamo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prestamo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_proyector = new SelectList(db.Proyectores, "id_proyector", "nombre", prestamo.id_proyector);
            return View(prestamo);
        }

        // GET: Prestamos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prestamo prestamo = db.Prestamos.Find(id);
            if (prestamo == null)
            {
                return HttpNotFound();
            }
            return View(prestamo);
        }

        // POST: Prestamos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Prestamo prestamo = db.Prestamos.Find(id);
            db.Prestamos.Remove(prestamo);
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
