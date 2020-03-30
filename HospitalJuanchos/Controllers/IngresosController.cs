using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HospitalJuanchos.Models;

namespace HospitalJuanchos.Controllers
{
    public class IngresosController : Controller
    {
        private BDContext db = new BDContext();

        // GET: Ingresos
        public ActionResult Index()
        {
            var ingresos = db.Ingresos.Include(i => i.Habitacion).Include(i => i.Paciente);
            return View(ingresos.ToList());
        }
        [HttpPost]
        public ActionResult Index(string busqueda, string select)
        {
            if (busqueda == string.Empty)
            {
                var ingresos = db.Ingresos.Include(c => c.Habitacion).Include(c => c.Paciente);
                return View(ingresos.ToList());
            }
            else if (select == string.Empty)
            {
                var ingresos = db.Ingresos.Include(c => c.Habitacion).Include(c => c.Paciente);
                return View(ingresos.ToList());
            }

            else if (select == "Num_Hab")
            {
                int s = (from g in db.Habitaciones where g.Numero_Hab == busqueda select g.ID_Habitacion).SingleOrDefault();
                //var ext = (from ex in db.Habitaciones where ex.Num_Habitacion == busqueda select ex).First().ID_Habitacion;
                //string x = ext.ToString();
                //int y = int.Parse(x);

                var ingresos = db.Ingresos.Include(c => c.Habitacion).Include(c => c.Paciente).Where(a => a.ID_Habitacion.Equals(s));
                return View(ingresos.ToList());
            }
            else if (select == "Fecha_De_Ingreso")
            {
                var ingresos = db.Ingresos.Include(c => c.Habitacion).Include(c => c.Paciente).Where(a => a.Fecha_De_Ingreso == busqueda);
                return View(ingresos.ToList());
            }

            return View(db.Ingresos.ToList());


        }

        // GET: Ingresos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ingresos ingresos = db.Ingresos.Find(id);
            if (ingresos == null)
            {
                return HttpNotFound();
            }
            return View(ingresos);
        }

        // GET: Ingresos/Create
        public ActionResult Create()
        {
            ViewBag.ID_Habitacion = new SelectList(db.Habitaciones, "ID_Habitacion", "Numero_Hab");
            ViewBag.ID_Paciente = new SelectList(db.Pacientes, "ID_Paciente", "ID_Paciente");
            return View();
        }

        // POST: Ingresos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Ingresos,ID_Habitacion,ID_Paciente,Fecha_De_Ingreso")] Ingresos ingresos)
        {
            if (ModelState.IsValid)
            {
                db.Ingresos.Add(ingresos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Habitacion = new SelectList(db.Habitaciones, "ID_Habitacion", "Numero_Hab", ingresos.ID_Habitacion);
            ViewBag.ID_Paciente = new SelectList(db.Pacientes, "ID_Paciente", "Nombre_Pac", ingresos.ID_Paciente);
            return View(ingresos);
        }

        // GET: Ingresos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ingresos ingresos = db.Ingresos.Find(id);
            if (ingresos == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Habitacion = new SelectList(db.Habitaciones, "ID_Habitacion", "Numero_Hab", ingresos.ID_Habitacion);
            ViewBag.ID_Paciente = new SelectList(db.Pacientes, "ID_Paciente", "Nombre_Pac", ingresos.ID_Paciente);
            return View(ingresos);
        }

        // POST: Ingresos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Ingresos,ID_Habitacion,ID_Paciente,Fecha_De_Ingreso")] Ingresos ingresos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ingresos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Habitacion = new SelectList(db.Habitaciones, "ID_Habitacion", "Numero_Hab", ingresos.ID_Habitacion);
            ViewBag.ID_Paciente = new SelectList(db.Pacientes, "ID_Paciente", "Nombre_Pac", ingresos.ID_Paciente);
            return View(ingresos);
        }

        // GET: Ingresos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ingresos ingresos = db.Ingresos.Find(id);
            if (ingresos == null)
            {
                return HttpNotFound();
            }
            return View(ingresos);
        }

        // POST: Ingresos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ingresos ingresos = db.Ingresos.Find(id);
            db.Ingresos.Remove(ingresos);
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
