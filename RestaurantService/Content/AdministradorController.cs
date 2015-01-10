using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestaurantService.Controllers
{
    public class AdministratorController : Controller
    {
        // GET: Administrador
        public ActionResult Mantenimientos()
        {
            return View();
        }

    }
}