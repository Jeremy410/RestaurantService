using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RestaurantService.Models;
using System.IO;

namespace RestaurantService.Controllers
{
    public class ComidasController : Controller
    {
        private RestaurantServiceDb db = new RestaurantServiceDb();

        // GET: Comidas
        public ActionResult Index()
        {
            var comidas = db.Comidas.Include(c => c.Categoria);
            return ValidarGet(View(comidas.ToList()));
        }

        // GET: Comidas/Create
        public ActionResult Create()
        {
            ViewBag.IdCategoria = new SelectList(db.Categorias, "Id", "Nombre");
            return ValidarGet(View());
        }

        [HttpPost]
        public ActionResult Crear()
        {
            string NombreTemp = Request.Form["Nombre"];
            string DetalleTemp = Request.Form["Detalle"];
            string PortadaTemp = "/Content/comidasImagenes/no_image.jpg";
            int PrecioTemp = int.Parse(Request.Form["Precio"]);
            int IdCategoriaTemp = int.Parse(Request.Form["IdCategoria"]);

            
            try
            {
                //Moving Image
                var Imagen = Request.Files["fileUpload"];
                Imagen.SaveAs(Server.MapPath("/Content/comidasImagenes/") + Path.GetFileName(Imagen.FileName));
                PortadaTemp = "/Content/comidasImagenes/" + Path.GetFileName(Imagen.FileName);
            }catch (System.IO.DirectoryNotFoundException) { }

            db.Comidas.Add(new Comida() { Nombre = NombreTemp, Detalle = DetalleTemp, Portada = PortadaTemp, Precio = PrecioTemp, IdCategoria = IdCategoriaTemp});
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Comidas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comida comida = db.Comidas.Find(id);
            if (comida == null)
            {
                return HttpNotFound();
            }

            ViewBag.IdCategoria = new SelectList(db.Categorias, "Id", "Nombre", comida.IdCategoria);
            return ValidarGet(View(comida));
        }

        // POST: Comidas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Detalle,Portada,Precio,IdCategoria")] Comida comida)
        {
            try
            {
                //RUTA-NUEVA-INPUT
                var Imagen = Request.Files["NuevaPortada"];
                Imagen.SaveAs(Server.MapPath("/Content/comidasImagenes/") + Path.GetFileName(Imagen.FileName));
                comida.Portada = "/Content/comidasImagenes/" + Path.GetFileName(Imagen.FileName);

            }
            catch (System.IO.DirectoryNotFoundException)
            {
                //RUTA-ANTIGUA BD
                comida.Portada = Request.Form["RutaPortada"];
            }

            if (ModelState.IsValid)
            {
                db.Entry(comida).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCategoria = new SelectList(db.Categorias, "Id", "Nombre", comida.IdCategoria);
            return View(comida);
        }

        // GET: Comidas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comida comida = db.Comidas.Find(id);
            if (comida == null)
            {
                return HttpNotFound();
            }
            return ValidarGet(View(comida));
        }

        // POST: Comidas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comida comida = db.Comidas.Find(id);
            db.Comidas.Remove(comida);
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
