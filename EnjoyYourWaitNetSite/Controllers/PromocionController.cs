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
    }
}