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
using System.Globalization;

namespace Proyectores.Controllers
{
    [Authorize]
    public class PrestamosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Prestamos
        public ActionResult Index()
        {
            
            return View();
        }
        
        [HttpGet]
        public JsonResult getPrestamos()
        {
            var prest = from item in db.Prestamos
                        select new {Proyector = item.Proyector.Marca.marca + " "+ item.Proyector.nombre,
                        Docente = item.Docente.Nombre+" " + item.Docente.Apellido,
                        Responsable = item.Responsable.Nombre + " "+ item.Responsable.Apellidos,
                        fecha = item.fecha,
                        activo = item.activo,
                        finalizado = item.finalizado,
                        id_prestamo = item.id_prestamo
                        };

            return Json(new {data = prest }, JsonRequestBehavior.AllowGet);
        }

        // GET: Prestamos/Create
        public ActionResult Create(int? id)
        {

            ViewBag.id_proyector = new SelectList(db.Proyectores, "id_proyector", "nombre");
            ViewBag.id_docente = new SelectList(db.Docentes, "DocenteId", "Nombre");
            if (id==0)
            {
                return View(new Prestamo());
            }else
            {
                var prest = db.Prestamos
                            .Include(p => p.Docente)
                            .Include(p => p.Proyector)
                            .FirstOrDefault(x => x.id_docente == id)
                            ;
                return View(prest);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_prestamo,id_proyector,id_docente,id_responsable,fecha,prestamo_anulado,finalizado,activo")] Prestamo prestamo)
        {
            var userID = User.Identity.GetUserId();
            prestamo.id_responsable = userID;
            string mensaje = "Error en la validación de los datos";
            bool band = false;

            prestamo.Docente = db.Docentes.FirstOrDefault(x => x.DocenteId == prestamo.id_docente);
            prestamo.Responsable = db.Users.FirstOrDefault(x => x.Id == prestamo.id_responsable);
            prestamo.Proyector = db.Proyectores.FirstOrDefault(x => x.id_proyector == prestamo.id_proyector);
            var fecha = Request.Form["fecha"];

            prestamo.fecha = DateTime.ParseExact(fecha, "d", CultureInfo.InvariantCulture);
            if (ModelState.IsValid)
            {

                if (prestamo.id_prestamo == 0)
                {
                    prestamo.activo = true;
                    prestamo.finalizado = false;
                    db.Prestamos.Add(prestamo);
                    db.SaveChanges();
                    mensaje = "Prestamo Guardado Correctamente";
                    band = true;
                }
                else
                {
                    //A la juerza
                    Prestamo tmp = db.Prestamos.Find(prestamo.id_prestamo);
                    tmp.id_proyector = prestamo.id_proyector;
                    tmp.id_responsable = prestamo.id_responsable;
                    tmp.fecha = prestamo.fecha;
                    tmp.activo = prestamo.activo;
                    tmp.Docente = prestamo.Docente;
                    tmp.Responsable = prestamo.Responsable;
                    tmp.id_responsable = User.Identity.GetUserId();
                    db.Entry(tmp).State = EntityState.Modified;
                    db.SaveChanges();
                    //db.Entry(prestamo).State = EntityState.Modified;  Esto genera un error desconocido
                    //db.SaveChanges();
                    mensaje = "Prestamo Actualizado Correctamente";
                    band = true;
                }
            }

            ViewBag.id_proyector = new SelectList(db.Proyectores, "id_proyector", "nombre", prestamo.id_proyector);
            ViewBag.id_docente = new SelectList(db.Docentes, "DocenteId", "Nombre");

            return Json(new { message = mensaje, success = band }, JsonRequestBehavior.AllowGet);
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
            prestamo.activo = false;
            db.Entry(prestamo).State = EntityState.Modified;
            db.SaveChanges();
            return Json(new { data = "Eliminado Correctamente", success = true }, JsonRequestBehavior.AllowGet);
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
