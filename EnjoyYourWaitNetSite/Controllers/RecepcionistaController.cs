using EnjoyYourWaitNetSite.BusinessLogic;
using EnjoyYourWaitNetSite.Entities;
using EnjoyYourWaitNetSite.Models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EnjoyYourWaitNetSite.Controllers
{
    public class RecepcionistaController : Controller
    {
        private BSRecepcionista bsRecepcionista = new BSRecepcionista();

        public async Task<ActionResult> GestionRecepcionista()
        {
            Usuario recepcionista = new Usuario();
            try
            {
                ViewBag.SuccessState = TempData["SuccessState"];
                //Cargo lista de recepcionistas
                //List<Usuario> recepcionistas = await bsRecepcionista.GetAllRecepcionistas();
                //return View("GestionRecepcionista", new RecepcionistaViewModel()
                //{
                //    lstRecepcionista = recepcionistas.ConvertAll(r => new Usuario
                //    {
                //        Dni = r.Dni,
                //        Nombre = r.Nombre,
                //        Apellido = r.Apellido,
                //        Email = r.Email,
                //        FechaNacimiento = r.FechaNacimiento
                //    })
                //});
                RecepcionistaViewModel model = new RecepcionistaViewModel();
                model.lstRecepcionista = new List<Usuario>();
                //Usuario recepcionista = new Usuario();
                recepcionista.Dni = 123;
                recepcionista.Nombre = "Nico";
                recepcionista.Apellido = "Vitale";
                recepcionista.Email = "EmailBienPiola";
                recepcionista.FechaNacimiento = DateTime.Now;
                model.lstRecepcionista.Add(recepcionista);
                return View("GestionRecepcionista", model);
            }
            catch (Exception)
            {
                TempData["SuccessState"] = "LOAD_FAILED";
                return View("RegistroRecepcionista", recepcionista);
            }
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

        public async Task<ActionResult> DeshabilitarRecepcionista(int dni)
        {
            //string token = HttpContext.Session.GetString("AuthToken");
            //if (token == null)
            //{
            //    return RedirectToAction("Index",
            //        "Authentication");
            //}

            try
            {
                TempData["SuccessState"] = "DISABLE_FAILED";
                bool result = await bsRecepcionista.DisableRecepcionista(dni);
                if (result)
                {
                    TempData["SuccessState"] = "DISABLE_SUCCESS";
                }
                return RedirectToAction("GestionRecepcionista");
            }
            catch (Exception)
            {
                TempData["SuccessState"] = "DISABLE_FAILED";
                return RedirectToAction("GestionRecepcionista");
            }
        }

        public async Task<ActionResult> HabilitarRecepcionista(int dni)
        {
            //string token = HttpContext.Session.GetString("AuthToken");
            //if (token == null)
            //{
            //    return RedirectToAction("Index",
            //        "Authentication");
            //}

            try
            {
                TempData["SuccessState"] = "ENABLE_FAILED";
                bool result = await bsRecepcionista.EnableRecepcionista(dni);
                if (result)
                {
                    TempData["SuccessState"] = "ENABLE_SUCCESS";
                }
                return RedirectToAction("GestionRecepcionista");
            }
            catch (Exception)
            {
                TempData["SuccessState"] = "ENABLE_FAILED";
                return RedirectToAction("GestionRecepcionista");
            }
        }

        public async Task<ActionResult> ModificarRecepcionista(int dni, string email)
        {
            //string token = HttpContext.Session.GetString("AuthToken");
            //if (token == null)
            //{
            //    return RedirectToAction("Index",
            //        "Authentication");
            //}

            try
            {
                TempData["SuccessState"] = "UPDATE_FAILED";
                bool isValid = Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
                if(isValid)
                {
                    bool result = await bsRecepcionista.UpdateRecepcionista(dni, email);
                    if (result)
                    {
                        TempData["SuccessState"] = "UPDATE_SUCCESS";
                    }
                }
                
                return RedirectToAction("GestionRecepcionista");
            }
            catch (Exception)
            {
                TempData["SuccessState"] = "UPDATE_FAILED";
                return RedirectToAction("GestionRecepcionista");
            }
        }
    }
}