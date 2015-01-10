using RestaurantService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestaurantService.Controllers
{
    public class DatosController : Controller
    {
        RestaurantServiceDb db = new RestaurantServiceDb();
        //
        // GET: /Login
        public ActionResult Login(string usuario, string contrasena)
        {
             var consulta = from r in db.Usuarios
                       where r.Entrada == usuario && r.Clave == contrasena
                       select r;
            if (consulta.Count() > 0)
            {
                Session["entrada"] = consulta.First().Entrada;
                Session["TipoUsuario"] = consulta.First().TipoId;
                ViewBag.usuario = consulta.First().Entrada;
                return PartialView("_Bienvenida");
            }
            ViewBag.Error = "Usuario o contraseña incorrecto.";
            return PartialView("../Shared/_LoginCampos");
        }

        public ActionResult Logout()
        {
            Session["entrada"] = null;
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Registrar()
        {
            return View();
        }
	}
}