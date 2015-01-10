using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestaurantService.Controllers
{
    public class AdministradorController : Controller
    {
        //
        // GET: /Administrador/
        public ActionResult Mantenimientos()
        {
            return ValidarGet(View());
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