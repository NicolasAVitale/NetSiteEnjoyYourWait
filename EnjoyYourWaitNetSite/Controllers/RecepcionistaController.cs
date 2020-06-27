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
            RecepcionistaViewModel model = new RecepcionistaViewModel();
            model.lstRecepcionista = new List<Usuario>();
            try
            {
                ViewBag.SuccessState = TempData["SuccessState"];
                //Cargo lista de recepcionistas
                List<Usuario> recepcionistas = await bsRecepcionista.GetAllRecepcionistas();
                model.lstRecepcionista = recepcionistas;
                if (recepcionistas.Count == 0)
                {
                    TempData["SuccessState"] = "LOAD_NOUSERS";
                }
                return View("GestionRecepcionista", model);
                //RecepcionistaViewModel model = new RecepcionistaViewModel();
                //model.lstRecepcionista = new List<Usuario>();
                ////Usuario recepcionista = new Usuario();
                //recepcionista.Dni = 12345678;
                //recepcionista.Nombre = "Nico";
                //recepcionista.Apellido = "Vitale";
                //recepcionista.Email = "enjoyyourwait@eyw.com";
                //recepcionista.FechaNacimiento = DateTime.Now;
                //model.lstRecepcionista.Add(recepcionista);
                //return View("GestionRecepcionista", model);
            }
            catch (Exception)
            {
                TempData["SuccessState"] = "LOAD_FAILED";
                return View("GestionRecepcionista", model);
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
                bool difEdad = difDateYear >= 17;
                if (ModelState.IsValid && difEdad)
                {
                    ViewBag.Success = false;
                    bool result = await bsRecepcionista.CreateRecepcionista(new Usuario()
                    {
                        dni = int.Parse(recepcionista.Dni),
                        nombre = recepcionista.Nombre,
                        apellido = recepcionista.Apellido,
                        email = recepcionista.Email,
                        fechaNacimiento = recepcionista.FechaNacimiento.ToString("yyyy-MM-dd"),
                        contrasena = recepcionista.Dni.ToString()
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

        public async Task<ActionResult> DeshabilitarRecepcionista(int id)
        {
            try
            {
                TempData["SuccessState"] = "DISABLE_FAILED";
                bool result = await bsRecepcionista.DisableRecepcionista(id);
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

        public async Task<ActionResult> HabilitarRecepcionista(int id)
        {
            try
            {
                TempData["SuccessState"] = "ENABLE_FAILED";
                bool result = await bsRecepcionista.EnableRecepcionista(id);
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

        public async Task<ActionResult> ModificarRecepcionista(int id, string email)
        {
            try
            {
                TempData["SuccessState"] = "UPDATE_FAILED";
                bool isValid = Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
                if(isValid)
                {
                    bool result = await bsRecepcionista.UpdateRecepcionista(id, new Usuario(){ email = email});
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