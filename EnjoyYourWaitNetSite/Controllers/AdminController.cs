using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EnjoyYourWaitNetSite.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Panel()
        {
            return View();
        }

        public ActionResult Usuarios()
        {
            return View();
        }

    }
}