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
    public class CategoriasController : Controller
    {
        private RestaurantServiceDb db = new RestaurantServiceDb();

        // GET: Categorias
        public ActionResult Index()
        {
            return ValidarGet(View(db.Categorias.ToList()));
        }

        // GET: Categorias/Create
        [HttpGet]
        public ActionResult Create()
        {
           return ValidarGet(View());
        }

        [HttpPost]
        public ActionResult Crear()
        {
            string NombreTemp = Request.Form["Nombre"];
            string PortadaTemp = "/Content/categoriasImagenes/no_image.jpg";

            try
            {
                //Moving Image
                var Imagen = Request.Files["fileUpload"];
                Imagen.SaveAs(Server.MapPath("/Content/categoriasImagenes/") + Path.GetFileName(Imagen.FileName));
                PortadaTemp = "/Content/categoriasImagenes/" + Path.GetFileName(Imagen.FileName);
            }
            catch (System.IO.DirectoryNotFoundException){}

            db.Categorias.Add(new Categoria(){ Nombre = NombreTemp, Portada = PortadaTemp});
            db.SaveChanges();

            return RedirectToAction("Index"); 
        }

        // GET: Categorias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = db.Categorias.Find(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return ValidarGet(View(categoria));
        }

        // POST: Categorias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id, Nombre")] Categoria categoria)
        {
            try
            {
                //RUTA-NUEVA-INPUT
               var Imagen = Request.Files["NuevaPortada"];
               Imagen.SaveAs(Server.MapPath("/Content/categoriasImagenes/") + Path.GetFileName(Imagen.FileName));
               categoria.Portada = "/Content/categoriasImagenes/" + Path.GetFileName(Imagen.FileName);

            }catch(System.IO.DirectoryNotFoundException ){
                //RUTA-ANTIGUA BD
                categoria.Portada = Request.Form["RutaPortada"];
            }
      
            if (ModelState.IsValid)
            {
                db.Entry(categoria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categoria);
        }

        // GET: Categorias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = db.Categorias.Find(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return ValidarGet(View(categoria));
        }

        // POST: Categorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Categoria categoria = db.Categorias.Find(id);
            db.Categorias.Remove(categoria);
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
