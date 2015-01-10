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
    public class TipoUsuarioController : Controller
    {
        private RestaurantServiceDb db = new RestaurantServiceDb();

        // GET: /TipoUsuario/
        public ActionResult Index()
        {
            return ValidarGet(View(db.Tipo_personas.ToList()));
        }

        // GET: /TipoUsuario/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipo_Usuario tipo_usuario = db.Tipo_personas.Find(id);
            if (tipo_usuario == null)
            {
                return HttpNotFound();
            }
            return ValidarGet(View(tipo_usuario));
        }

        // GET: /TipoUsuario/Create
        public ActionResult Create()
        {
            return ValidarGet(View());
        }

        // POST: /TipoUsuario/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Nombre")] Tipo_Usuario tipo_usuario)
        {
            if (ModelState.IsValid)
            {
                db.Tipo_personas.Add(tipo_usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipo_usuario);
        }

        // GET: /TipoUsuario/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipo_Usuario tipo_usuario = db.Tipo_personas.Find(id);
            if (tipo_usuario == null)
            {
                return HttpNotFound();
            }
            return ValidarGet(View(tipo_usuario));
        }

        // POST: /TipoUsuario/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Nombre")] Tipo_Usuario tipo_usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipo_usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipo_usuario);
        }

        // GET: /TipoUsuario/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipo_Usuario tipo_usuario = db.Tipo_personas.Find(id);
            if (tipo_usuario == null)
            {
                return HttpNotFound();
            }
            return ValidarGet(View(tipo_usuario));
        }

        // POST: /TipoUsuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tipo_Usuario tipo_usuario = db.Tipo_personas.Find(id);
            db.Tipo_personas.Remove(tipo_usuario);
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
