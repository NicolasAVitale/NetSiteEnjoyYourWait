using EnjoyYourWaitNetSite.BusinessLogic;
using EnjoyYourWaitNetSite.Entities;
using EnjoyYourWaitNetSite.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EnjoyYourWaitNetSite.Controllers
{
    public class PromocionController : Controller
    {
        private BSPromocion bsPromocion = new BSPromocion();
        private BSProducto bsProducto = new BSProducto();

        public ActionResult Promociones()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AgregarProducto()
        {
            return View();
        }

        public ActionResult ModificarProducto()
        {
            return View();
        }

        public ActionResult EliminarProducto()
        {
            return View();
        }
       
        public async Task<ActionResult> GestionPromocion()
        {
            PromocionesViewModel model = new PromocionesViewModel();
            model.lstPromociones = new List<Promocion>();
            try
            {
                ViewBag.SuccessState = TempData["SuccessState"];

                //Cargo lista de promociones
                List<Promocion> promociones = await bsPromocion.GetAllPromociones();
                model.lstPromociones = promociones;
                if (promociones.Count == 0)
                {
                    TempData["SuccessState"] = "LOAD_NOPROMO";
                }
                return View("GestionPromocion", model);
            }
            catch (Exception)
            {
                TempData["SuccessState"] = "LOAD_FAILED";
                return View("GestionPromocion", model);
            }
        }

        public ActionResult AbrirRegistroPromocion()
        {
            AddPromocionViewModel model = new AddPromocionViewModel();
            return View("RegistroPromocion", model);
        }

        public async Task<ActionResult> AñadirPromocion(AddPromocionViewModel promocion)
        {
            try
            {
                ViewBag.Success = null;
                bool fechaInicioValida = promocion.FechaInicio >= DateTime.Now.Date;
                bool fechaBajaValida = promocion.FechaBaja >= promocion.FechaInicio;
                if (ModelState.IsValid && fechaInicioValida && fechaBajaValida)
                {
                    ViewBag.Success = false;
                    bool result = await bsPromocion.CreatePromocion(new Promocion()
                    {
                       descripcion = promocion.Descripcion,
                       fechaInicio = promocion.FechaInicio.ToString("yyyy-MM-dd"),
                       fechaBaja = promocion.FechaBaja.ToString("yyyy-MM-dd"),
                       esPremio = promocion.EsPremio
                    });
                    if (result)
                    {
                        TempData["Success"] = true;
                        return RedirectToAction("GestionPromocion");
                    }
                }
                if (!fechaInicioValida)
                {
                    ViewBag.FechaInicioInvalida = "La fecha de inicio de la promoción debe igual o posterior o la fecha de hoy";
                }
                if (!fechaBajaValida)
                {
                    ViewBag.FechaBajaInvalida = "La fecha de vencimiento de la promoción debe igual o posterior o la fecha de inicio";
                }

                return View("RegistroPromocion", promocion);
            }
            catch (Exception)
            {
                TempData["Success"] = false;
                return View("RegistroPromocion", promocion);
            }
        }

        public ActionResult ObtenerTiposPromocion()
        {
            List<TipoProducto> lista = new List<TipoProducto>();
            lista.Add(new TipoProducto { IdTipo = 0, Descripcion = "Promocion" });
            lista.Add(new TipoProducto { IdTipo = 1, Descripcion = "Premio" });
            return Json(lista);
        }
    }
}