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
            var proyectores = db.Proyectores.Include(p => p.Estado).Include(p => p.Marca);
            return View(proyectores.ToList());
        }

        // GET: Proyectors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proyector proyector = db.Proyectores.Find(id);
            if (proyector == null)
            {
                return HttpNotFound();
            }
            return View(proyector);
        }

        // GET: Proyectors/Create
        public ActionResult Create()
        {
            ViewBag.id_estado = new SelectList(db.Estados, "id_estado", "nombre");
            ViewBag.id_marca = new SelectList(db.Marca, "id_marca", "marca");
            return View();
        }

        // POST: Proyectors/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_proyector,id_marca,id_estado,nombre,activo")] Proyector proyector)
        {
            if (ModelState.IsValid)
            {
                db.Proyectores.Add(proyector);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_estado = new SelectList(db.Estados, "id_estado", "nombre", proyector.id_estado);
            ViewBag.id_marca = new SelectList(db.Marca, "id_marca", "marca", proyector.id_marca);
            return View(proyector);
        }

        // GET: Proyectors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proyector proyector = db.Proyectores.Find(id);
            if (proyector == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_estado = new SelectList(db.Estados, "id_estado", "nombre", proyector.id_estado);
            ViewBag.id_marca = new SelectList(db.Marca, "id_marca", "marca", proyector.id_marca);
            return View(proyector);
        }

        // POST: Proyectors/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_proyector,id_marca,id_estado,nombre,activo")] Proyector proyector)
        {
            if (ModelState.IsValid)
            {
                db.Entry(proyector).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_estado = new SelectList(db.Estados, "id_estado", "nombre", proyector.id_estado);
            ViewBag.id_marca = new SelectList(db.Marca, "id_marca", "marca", proyector.id_marca);
            return View(proyector);
        }

        // GET: Proyectors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proyector proyector = db.Proyectores.Find(id);
            if (proyector == null)
            {
                return HttpNotFound();
            }
            return View(proyector);
        }

        // POST: Proyectors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Proyector proyector = db.Proyectores.Find(id);
            db.Proyectores.Remove(proyector);
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
