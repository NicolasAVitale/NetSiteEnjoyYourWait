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


        
        public ActionResult AbrirRegistroRecepcionista()
        {
            AddRecepcionistaViewModel model = new AddRecepcionistaViewModel();
            return View("RegistroRecepcionista", model);
        }

        public async Task<ActionResult> AñadirRecepcionista(AddRecepcionistaViewModel recepcionista)
        {
            try
            {
                ViewBag.Success = null;
                int difDateYear = DateTime.Today.Year - recepcionista.FechaNacimiento.Year;
                int difDateMonth = DateTime.Today.Month - recepcionista.FechaNacimiento.Month;
                int difDateDay = DateTime.Today.Day - recepcionista.FechaNacimiento.Day;
                bool difEdad = difDateYear >= 17 && difDateMonth >= 0 && difDateDay >= 0;
                if (ModelState.IsValid && difEdad)
                {
                    ViewBag.Success = false;
                    bool result = await bsRecepcionista.CreateRecepcionista(new Usuario()
                    {
                        Dni = int.Parse(recepcionista.Dni),
                        Nombre = recepcionista.Nombre,
                        Apellido = recepcionista.Apellido,
                        Email = recepcionista.Email,
                        FechaNacimiento = recepcionista.FechaNacimiento,
                        Contrasena = recepcionista.Dni.ToString()
                    });
                    if (result)
                    {
                        TempData["Success"] = true;
                        return RedirectToAction("GestionRecepcionista");
                    }
                }
                if (!difEdad)
                {
                    ViewBag.InvalidAge = "La edad minima requerida debe ser al menos 17 años";
                }

                return View("RegistroRecepcionista", recepcionista);
            }
            catch (Exception)
            {
                TempData["Success"] = false;
                return View("RegistroRecepcionista", recepcionista);
            }
        }

        public async Task<ActionResult> EliminarRecepcionista(int dni)
        {
            //string token = HttpContext.Session.GetString("AuthToken");
            //if (token == null)
            //{
            //    return RedirectToAction("Index",
            //        "Authentication");
            //}

            try
            {
                TempData["SuccessState"] = "DELETE_FAILED";
                bool result = await bsRecepcionista.DeleteRecepcionista(dni);
                if (result)
                {
                    TempData["SuccessState"] = "DELETE_SUCCESS";
                }
                return RedirectToAction("GestionRecepcionista");
            }
            catch (Exception)
            {
                TempData["SuccessState"] = "DELETE_FAILED";
                return RedirectToAction("GestionRecepcionista");
            }
        }

        public ActionResult ModificarRecepcionista()
        {
            return View();
        }
    }
}