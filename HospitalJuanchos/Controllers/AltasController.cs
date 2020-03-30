﻿using System;
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
    public class AltasController : Controller
    {
        private BDContext db = new BDContext();

        // GET: Altas
        public ActionResult Index()
        {
            var altas = db.Altas.Include(a => a.Ingresos);
            return View(altas.ToList());
        }

        // GET: Altas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Altas altas = db.Altas.Find(id);
            if (altas == null)
            {
                return HttpNotFound();
            }
            return View(altas);
        }

        // GET: Altas/Create
        public ActionResult Create()
        {
            ViewBag.ID_Ingreso = new SelectList(db.Ingresos, "ID_Ingresos", "ID_Ingresos");
            return View();
        }

        // POST: Altas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_ALtas,Nombre_De_Paciente,Fecha_De_Ingreso,Fecha_De_Salida,Habitacion,Monto,ID_Ingreso")] Altas altas)
        {
            if (ModelState.IsValid)
            {
                db.Altas.Add(altas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Ingreso = new SelectList(db.Ingresos, "ID_Ingresos", "ID_Ingresos", altas.ID_Ingreso);
            return View(altas);
        }

        // GET: Altas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Altas altas = db.Altas.Find(id);
            if (altas == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Ingreso = new SelectList(db.Ingresos, "ID_Ingresos", "ID_Ingresos", altas.ID_Ingreso);
            return View(altas);
        }

        // POST: Altas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_ALtas,Nombre_De_Paciente,Fecha_De_Ingreso,Fecha_De_Salida,Habitacion,Monto,ID_Ingreso")] Altas altas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(altas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Ingreso = new SelectList(db.Ingresos, "ID_Ingresos", "Fecha_De_Ingreso", altas.ID_Ingreso);
            return View(altas);
        }

        // GET: Altas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Altas altas = db.Altas.Find(id);
            if (altas == null)
            {
                return HttpNotFound();
            }
            return View(altas);
        }

        // POST: Altas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Altas altas = db.Altas.Find(id);
            db.Altas.Remove(altas);
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
        [HttpPost]
        public JsonResult NombrePac(int clavePaciente)
        {
            var duplicado = (from i in db.Ingresos
                             join p in db.Pacientes
                             on i.ID_Paciente equals p.ID_Paciente
                             where i.ID_Ingresos == clavePaciente
                             select p.Nombre_Pac).ToList();
            return Json(duplicado);
        }

        public JsonResult MontoT(int clavePaciente)
        {

            var duplicado = (from i in db.Ingresos
                             join h in db.Habitaciones
                             on i.ID_Habitacion equals h.ID_Habitacion
                             where i.ID_Ingresos == clavePaciente
                             select h.Precio).ToList();
            return Json(duplicado);
        }

        public JsonResult FechaDeIngreso(int clavePaciente)
        {

            var duplicado = (from i in db.Ingresos
                             where i.ID_Ingresos == clavePaciente
                             select i.Fecha_De_Ingreso).ToList();
            return Json(duplicado);
        }

        public JsonResult NumHabitacion(int clavePaciente)
        {

            var duplicado = (from i in db.Ingresos
                             join h in db.Habitaciones
                             on i.ID_Habitacion equals h.ID_Habitacion
                             where i.ID_Ingresos == clavePaciente
                             select h.Numero_Hab).ToList();
            return Json(duplicado);
        }
    }
}
    
