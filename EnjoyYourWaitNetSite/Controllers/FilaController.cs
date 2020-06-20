using com.sun.tools.javac.util;
using EnjoyYourWaitNetSite.BusinessLogic;
using EnjoyYourWaitNetSite.Entities;
using EnjoyYourWaitNetSite.Helper;
using EnjoyYourWaitNetSite.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EnjoyYourWaitNetSite.Controllers
{
    public class FilaController : Controller
    {
        private BSFila bsFila = new BSFila();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EnviarCorreoConfirmacion(string email)
        {
            try
            {
                TempData["SuccessState"] = "SEND_FAILED";
                bool isValid = Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
                if (isValid)
                {
                    bsFila.EnviarCorreoConfirmacion(email);
                    SessionHelper.Email = email;
                    TempData["SuccessState"] = "SEND_SUCCESS";
                }
                else
                {
                    TempData["SuccessState"] = "MAIL_INCORRECT";
                }

                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                TempData["SuccessState"] = "SEND_FAILED";
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult ConfirmarIngreso()
        {
            string email = SessionHelper.Email;
            if (email != null)
            {
                ClienteViewModel model = new ClienteViewModel();
                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<ActionResult> AñadirClienteAFila(ClienteViewModel cliente)
        {
            try
            {
                ViewBag.Success = null;
                int difDateYear = DateTime.Today.Year - cliente.FechaNacimiento.Year;
                bool difEdad = difDateYear >= 17;
                if (ModelState.IsValid && difEdad)
                {
                    ViewBag.Success = false;

                    Cliente entidadCliente = new Cliente()
                    {
                        dni = int.Parse(cliente.Dni),
                        nombre = cliente.Nombre,
                        apellido = cliente.Apellido,
                        email = cliente.Email,
                        fechaNacimiento = cliente.FechaNacimiento.ToString("yyyy-MM-dd"),
                        activo = 1
                    };

                    Cliente clienteRegistrado = await bsFila.RegistrarCliente(entidadCliente);
                    if (clienteRegistrado != null)
                    {
                        SessionHelper.Cliente = clienteRegistrado;
                        bool result = await bsFila.IngresarAFila(clienteRegistrado.idCliente, int.Parse(cliente.CantComensales));
                        if (result)
                        {
                            TempData["SuccessState"] = "ENTRY_SUCCESS";
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
                if (!difEdad)
                {
                    ViewBag.InvalidAge = "La edad minima requerida debe ser al menos 17 años";
                }

                return View("ConfirmarIngreso", cliente);
            }
            catch (Exception)
            {
                TempData["SuccessState"] = "ENTRY_INCORRECT";
                return View("ConfirmarIngreso", cliente);
            }
        }

        public async Task<List<int>> CalcularTiempoYPersonasEspera()
        {
            List<int> espera = new List<int>();
            int capacidad = int.Parse(ConfigurationManager.AppSettings.Get("CapacidadRestaurante"));
            int tiempo = int.Parse(ConfigurationManager.AppSettings.Get("TiempoEstimado"));

            if (SessionHelper.Cliente != null)
            {
                espera = await bsFila.CalcularTiempoYPersonasEsperaCliente(SessionHelper.Cliente.idCliente, capacidad, tiempo);
            } else
            {
                espera = await bsFila.CalcularTiempoYPersonasEsperaGeneral(capacidad, tiempo);
            }

            return espera;
        }
    }
}