using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HospitalJuanchos.Models;
using Rotativa;

namespace HospitalJuanchos.Controllers
{
    public class CitasController : Controller
    {
        private BDContext db = new BDContext();

        // GET: Citas
        public ActionResult Index()
        {
            var citas = db.Citas.Include(c => c.Medico).Include(c => c.Paciente);
            return View(citas.ToList());
        }
        [HttpPost]
        public ActionResult Index(string busqueda, string select)
        {
            if (busqueda == string.Empty)
            {
                var citas = db.Citas.Include(c => c.Medico).Include(c => c.Paciente);
                return View(citas.ToList());
            }
            else if (select == string.Empty)
            {
                var citas = db.Citas.Include(c => c.Medico).Include(c => c.Paciente);
                return View(citas.ToList());
            }

            else if (select == "Nombre_Med")
            {
                int a = (from g in db.Medicos where g.Nombre_Med == busqueda select g.ID_Medico).SingleOrDefault();
               
                var citas = db.Citas.Include(c => c.Medico).Include(c => c.Paciente).Where(b => b.ID_Medico.Equals(a));
                return View(citas.ToList());

            }
            else if (select == "Nombre_Pac")
            {
                int a = (from g in db.Pacientes where g.Nombre_Pac == busqueda select g.ID_Paciente).SingleOrDefault();

                var citas = db.Citas.Include(c => c.Medico).Include(c => c.Paciente).Where(b => b.ID_Paciente.Equals(a));
                return View(citas.ToList());
            }
            else if (select == "Fecha_De_Cita")
            {

                var citas = db.Citas.Include(c => c.Paciente).Include(c => c.Medico).Where(a => a.Fecha_De_Cita == busqueda);
                return View(citas.ToList());
            }

            return View(db.Citas.ToList());

        }
        public ActionResult Imprimir()
        {
            var print = new ActionAsPdf("Index");
            return print;
        }

        // GET: Citas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Citas citas = db.Citas.Find(id);
            if (citas == null)
            {
                return HttpNotFound();
            }
            return View(citas);
        }

        // GET: Citas/Create
        public ActionResult Create()
        {
            ViewBag.ID_Medico = new SelectList(db.Medicos, "ID_Medico", "Nombre_Med");
            ViewBag.ID_Paciente = new SelectList(db.Pacientes, "ID_Paciente", "ID_Paciente");
            return View();
        }

        // POST: Citas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Cita,ID_Paciente,Fecha_De_Cita,Hora_De_Cita,ID_Medico")] Citas citas)
        {
            if (ModelState.IsValid)
            {
                db.Citas.Add(citas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Medico = new SelectList(db.Medicos, "ID_Medico", "Nombre_Med", citas.ID_Medico);
            ViewBag.ID_Paciente = new SelectList(db.Pacientes, "ID_Paciente", "Nombre_Pac", citas.ID_Paciente);
            return View(citas);
        }

        // GET: Citas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Citas citas = db.Citas.Find(id);
            if (citas == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Medico = new SelectList(db.Medicos, "ID_Medico", "Nombre_Med", citas.ID_Medico);
            ViewBag.ID_Paciente = new SelectList(db.Pacientes, "ID_Paciente", "Nombre_Pac", citas.ID_Paciente);
            return View(citas);
        }

        // POST: Citas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Cita,ID_Paciente,Fecha_De_Cita,Hora_De_Cita,ID_Medico")] Citas citas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(citas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Medico = new SelectList(db.Medicos, "ID_Medico", "Nombre_Med", citas.ID_Medico);
            ViewBag.ID_Paciente = new SelectList(db.Pacientes, "ID_Paciente", "Nombre_Pac", citas.ID_Paciente);
            return View(citas);
        }

        // GET: Citas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Citas citas = db.Citas.Find(id);
            if (citas == null)
            {
                return HttpNotFound();
            }
            return View(citas);
        }

        // POST: Citas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Citas citas = db.Citas.Find(id);
            db.Citas.Remove(citas);
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
