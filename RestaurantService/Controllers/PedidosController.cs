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
    public class PedidosController : Controller
    {
        private RestaurantServiceDb db = new RestaurantServiceDb();

        // GET: Pedidos
        public ActionResult Index()
        {
            var pedidos = db.Pedidos.Include(p => p.Mensajero).Include(p => p.Usuario);
            return ValidarGet(View(pedidos.ToList()));
        }

        // GET: Pedidos/Create
        public ActionResult Create()
        {
            ViewBag.IdMensajero = new SelectList(db.Mensajeros, "Id", "Nombre");
            ViewBag.IdUsuario = new SelectList(db.Usuarios, "Id", "Nombre");
            return ValidarGet(View());
        }

        // POST: Pedidos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id, IdMensajero,IdUsuario,FechaPedido,PrecioTotal,Estado")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                db.Pedidos.Add(pedido);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdMensajero = new SelectList(db.Mensajeros, "Id", "Nombre", pedido.IdMensajero);
            ViewBag.IdUsuario = new SelectList(db.Usuarios, "Id", "Nombre", pedido.IdUsuario);
            return View(pedido);
        }

        // GET: Pedidos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = db.Pedidos.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdMensajero = new SelectList(db.Mensajeros, "Id", "Nombre", pedido.IdMensajero);
            ViewBag.IdUsuario = new SelectList(db.Usuarios, "Id", "Nombre", pedido.IdUsuario);
            return ValidarGet(View(pedido));
        }

        // POST: Pedidos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdMensajero,IdUsuario,FechaPedido,PrecioTotal")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                pedido.Estado = Request.Form["Estado"];

                db.Entry(pedido).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdMensajero = new SelectList(db.Mensajeros, "Id", "Nombre", pedido.IdMensajero);
            ViewBag.IdUsuario = new SelectList(db.Usuarios, "Id", "Nombre", pedido.IdUsuario);
            return View(pedido);
        }

        // GET: Pedidos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = db.Pedidos.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            return ValidarGet(View(pedido));
        }

        // POST: Pedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pedido pedido = db.Pedidos.Find(id);
            db.Pedidos.Remove(pedido);
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
