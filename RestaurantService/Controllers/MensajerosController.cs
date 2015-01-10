using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RestaurantService.Models;

namespace RestaurantService.Controllers
{
    public class MensajerosController : Controller
    {
        private RestaurantServiceDb db = new RestaurantServiceDb();

        // GET: Mensajeros
        public ActionResult Index()
        {
            return ValidarGet(View(db.Mensajeros.ToList()));
        }

        // GET: Mensajeros/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mensajero mensajero = db.Mensajeros.Find(id);
            if (mensajero == null)
            {
                return HttpNotFound();
            }
            return ValidarGet(View(mensajero));
        }

        // GET: Mensajeros/Create
        public ActionResult Create()
        {
            return ValidarGet(View());
        }

        // POST: Mensajeros/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,Apellido,Direccion,Telefono")] Mensajero mensajero)
        {
            if (ModelState.IsValid)
            {
                db.Mensajeros.Add(mensajero);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mensajero);
        }

        // GET: Mensajeros/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mensajero mensajero = db.Mensajeros.Find(id);
            if (mensajero == null)
            {
                return HttpNotFound();
            }
            return ValidarGet(View(mensajero));
        }

        // POST: Mensajeros/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Apellido,Direccion,Telefono")] Mensajero mensajero)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mensajero).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mensajero);
        }

        // GET: Mensajeros/Delete/5
        public ActionResult Delete(int? id)
        {
             if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mensajero mensajero = db.Mensajeros.Find(id);
            if (mensajero == null)
            {
                return HttpNotFound();
            }
            return ValidarGet(View(mensajero));
        }

        // POST: Mensajeros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Mensajero mensajero = db.Mensajeros.Find(id);
            db.Mensajeros.Remove(mensajero);
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

        public ActionResult ValidarGet(ActionResult View)
        {
            //VALIDADOR GET BEGIN
            if (Session["entrada"] == null)
                return RedirectToAction("Index", "Home");

            if ((int)Session["TipoUsuario"] != 1)
                return RedirectToAction("Index", "Home");
            //VALIDADOR GET END

            return View;
        }
    }
}
