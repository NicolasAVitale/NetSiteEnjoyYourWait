using com.sun.tools.javac.util;
using EnjoyYourWaitNetSite.BusinessLogic;
using EnjoyYourWaitNetSite.Entities;
using EnjoyYourWaitNetSite.Helper;
using EnjoyYourWaitNetSite.Models;
using java.sql;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EnjoyYourWaitNetSite.Controllers
{
    public class RecepcionistaController : Controller
    {
        private BSHome bsHome = new BSHome();

        public ActionResult Index()
        {
            //string token = HttpContext.Session.GetString("AuthToken");
            //if (token == null)
            //{
            //    return RedirectToAction("Index",
            //        "Authentication");
            //}

            //if (idClient == null)
            //{
            //    return RedirectToAction("ClientSelection",
            //        "Client", new { target = "Resource" });
            //}
            //try
            //{
            ViewBag.SuccessState = TempData["SuccessState"];
            //Cargo lista de recepcionistas

            RecepcionistaViewModel model = new RecepcionistaViewModel();
            model.lstRecepcionista = new List<Recepcionista>();
            Recepcionista recepcionista = new Recepcionista();
            recepcionista.Dni = 123;
            recepcionista.Nombre = "Nico";
            recepcionista.Apellido = "Vitale";
            recepcionista.Email = "Email";
            recepcionista.FechaNacimiento = DateTime.Now;
            model.lstRecepcionista.Add(recepcionista);
            return View("Recepcionista", model);

            //}
            //catch (Exception)
            //{
            //    ViewBag.SuccessState = "LOAD_FAILED";
            //    return View("ClientResource", null);
            //}
        }

        public ActionResult RegistrarRecepcionista()
        {
            return View();
        }

        public ActionResult EliminarRecepcionista()
        {
            return View();
        }

        public ActionResult ModificarRecepcionista()
        {
            return View();
        }
    }
}