using RestaurantService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestaurantService.Controllers
{
    public class HomeController : Controller
    {
        private RestaurantServiceDb db = new RestaurantServiceDb();
        public ActionResult Index()
        {
            return View(db.Comidas.ToList());
        }

        public ActionResult Contacto()
        { 
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Categorias()
        {
            ViewBag.Message = "Categorias";

            var Categorias = from r in db.Categorias
                             select r;

            return View(Categorias);
        }

        public ActionResult DescripcionPlato()
        {
            return View();
        }

    }
}