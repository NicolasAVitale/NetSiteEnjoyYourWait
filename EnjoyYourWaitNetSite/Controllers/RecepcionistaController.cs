using EnjoyYourWaitNetSite.BusinessLogic;
using EnjoyYourWaitNetSite.Entities;
using EnjoyYourWaitNetSite.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EnjoyYourWaitNetSite.Controllers
{
    public class RecepcionistaController : Controller
    {
        private BSRecepcionista bsRecepcionista = new BSRecepcionista();

        public ActionResult GestionRecepcionista()
        {
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
            return View("GestionRecepcionista", model);
        }


        [HttpGet]
        public ActionResult AbrirRegistroRecepcionista()
        {
            AddRecepcionistaViewModel model = new AddRecepcionistaViewModel();
            return View("RegistroRecepcionista", model);
        }

        public async Task<ActionResult> AñadirRecepcionista(AddRecepcionistaViewModel recepcionista)
        {
            ViewBag.Success = null;
            if (ModelState.IsValid)
            {
                ViewBag.Success = false;
                bool result = await bsRecepcionista.CreateRecepcionista(new Recepcionista()
                {
                    Dni = recepcionista.Dni,
                    Nombre = recepcionista.Nombre,
                    Apellido = recepcionista.Apellido,
                    Email = recepcionista.Email,
                    FechaNacimiento = recepcionista.FechaNacimiento
                });
                if (result)
                {
                    TempData["Success"] = true;
                    return RedirectToAction("Registration", "Resource");
                }
            }

            return View("RegistroRecepcionista", recepcionista);
        }

        [HttpPost]
        public ActionResult EliminarRecepcionista(int dni)
        {
            TempData["SuccessState"] = "DELETE_FAILED";
            return RedirectToAction("GestionRecepcionista");
        }

        public ActionResult ModificarRecepcionista()
        {
            return View();
        }
    }
}