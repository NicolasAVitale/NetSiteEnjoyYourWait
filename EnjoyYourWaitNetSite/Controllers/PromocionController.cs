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

        public async Task<ActionResult> GestionPromocion()
        {
            PromocionesViewModel model = new PromocionesViewModel();
            model.lstPromociones = new List<Promocion>();
            try
            {
                ViewBag.SuccessState = TempData["SuccessState"];

                //Cargo lista de promociones
                List<Promocion> promociones = await bsPromocion.GetAllPromociones();

                foreach (var promocion in promociones)
                {
                    promocion.fechaInicio = DateTime.Parse(promocion.fechaInicio, null, System.Globalization.DateTimeStyles.RoundtripKind).ToString("dd/MM/yyyy");
                    promocion.fechaBaja = DateTime.Parse(promocion.fechaBaja, null, System.Globalization.DateTimeStyles.RoundtripKind).ToString("dd/MM/yyyy");
                }

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

        public async Task<ActionResult> DeshabilitarPromocion(int idPromocion)
        {
            try
            {
                TempData["SuccessState"] = "DISABLE_FAILED";
                bool result = await bsPromocion.DisablePromocion(idPromocion);
                if (result)
                {
                    TempData["SuccessState"] = "DISABLE_SUCCESS";
                }
                return RedirectToAction("GestionPromocion");
            }
            catch (Exception)
            {
                TempData["SuccessState"] = "DISABLE_FAILED";
                return RedirectToAction("GestionPromocion");
            }
        }

        public async Task<ActionResult> HabilitarPromocion(int idPromocion)
        {
            try
            {
                TempData["SuccessState"] = "ENABLE_FAILED";
                bool result = await bsPromocion.EnablePromocion(idPromocion);
                if (result)
                {
                    TempData["SuccessState"] = "ENABLE_SUCCESS";
                }
                return RedirectToAction("GestionPromocion");
            }
            catch (Exception)
            {
                TempData["SuccessState"] = "ENABLE_FAILED";
                return RedirectToAction("GestionPromocion");
            }
        }

        public async Task<ActionResult> VerDetallePromocion(Promocion promocion)
        {
            PromocionProductoViewModel model = new PromocionProductoViewModel();
            model.IdPromocion = promocion.idPromocion;
            model.lstProducto = new List<Producto>();
            try
            {
                ViewBag.SuccessState = TempData["SuccessState"];

                //Cargo lista de productos
                List<Producto> productos = await bsPromocion.GetAllProductosPromocion(promocion.idPromocion);
                model.lstProducto = productos;
                if (productos.Count == 0)
                {
                    model.lstProducto = await bsProducto.ObtenerProductosActivos();
                    TempData["SuccessState"] = "LOAD_NOPRODUCTS";
                    ViewBag.SuccessState = TempData["SuccessState"];
                }

                return View("GestionProductosPromocion", model);
            }
            catch (Exception)
            {
                TempData["SuccessState"] = "LOAD_FAILED";
                return View("GestionPromocion", model);
            }
        }
        public async Task<ActionResult> AsociarProductosPromocion(List<string> idProductos, int idPromocion)
        {
            try
            {
                ViewBag.SuccessState = TempData["SuccessState"];


                bool result = false;
                foreach (var idProducto in idProductos)
                {
                    result = await bsPromocion.AsociarProductosPromocion(idPromocion, new ProductoId() { idProducto = idProducto });
                }

                if (result)
                {
                    TempData["SuccessState"] = "LOAD_PRODUCTS_SUCCESSFULL";
                }
                else
                {
                    TempData["SuccessState"] = "LOAD_FAILED";
                }
                
                return null;
            }
            catch (Exception)
            {
                TempData["SuccessState"] = "LOAD_FAILED";
                return null;
            }
        }

        public ActionResult AbrirModificarPromocion(Promocion promocion)
        {
            string[] fechaInicioString = promocion.fechaInicio.Split('/');
            string[] fechaBajaString = promocion.fechaBaja.Split('/');
            promocion.fechaInicio = fechaInicioString[2] + "-" + fechaInicioString[1] + "-" + fechaInicioString[0];
            promocion.fechaBaja = fechaBajaString[2] + "-" + fechaBajaString[1] + "-" + fechaBajaString[0];
            
            UpdatePromocionViewModel model = new UpdatePromocionViewModel
            {
                Descripcion = promocion.descripcion,
                FechaInicio = promocion.fechaInicio,
                FechaBaja = promocion.fechaBaja,
                EsPremio = promocion.esPremio,
                IdPromocion = promocion.idPromocion
            };
            return View("ModificarPromocion", model);
        }

        public async Task<ActionResult> ModificarPromocion(UpdatePromocionViewModel promocion)
        {
            try
            {
                TempData["SuccessState"] = "UPDATE_FAILED";

                string[] fechaInicioString = promocion.FechaInicio.Split('-');
                string[] fechaBajaString = promocion.FechaBaja.Split('-');
                DateTime fechaInicio = new DateTime(Convert.ToInt32(fechaInicioString[0]), Convert.ToInt32(fechaInicioString[1]), Convert.ToInt32(fechaInicioString[2]));
                DateTime fechaBaja = new DateTime(Convert.ToInt32(fechaBajaString[0]), Convert.ToInt32(fechaBajaString[1]), Convert.ToInt32(fechaBajaString[2]));

                bool fechaInicioValida = fechaInicio >= DateTime.Now.Date;
                bool fechaBajaValida = fechaBaja >= fechaInicio;

                if (ModelState.IsValid && fechaInicioValida && fechaBajaValida)
                {
                    UpdatePromocionApiModel promocionApi = new UpdatePromocionApiModel()
                    {
                        descripcion = promocion.Descripcion,
                        fechaInicio = fechaInicio.ToString("yyyy-MM-dd HH:mm:ss"),
                        fechaBaja = fechaBaja.ToString("yyyy-MM-dd HH:mm:ss"),
                        esPremio = promocion.EsPremio
                    };
                    bool result = await bsPromocion.UpdatePromocion(promocion.IdPromocion, promocionApi);

                    if (result)
                    {
                        TempData["SuccessState"] = "UPDATE_SUCCESS";
                        return RedirectToAction("GestionPromocion");
                    }

                    return RedirectToAction("GestionPromocion");
                }

                if (!fechaInicioValida)
                {
                    ViewBag.FechaInicioInvalida = "La fecha de inicio de la promoción debe igual o posterior o la fecha de hoy";
                }
                if (!fechaBajaValida)
                {
                    ViewBag.FechaBajaInvalida = "La fecha de vencimiento de la promoción debe igual o posterior o la fecha de inicio";
                }

                return View("ModificarPromocion", promocion);
            }
            catch (Exception)
            {
                TempData["SuccessState"] = "UPDATE_FAILED";
                return RedirectToAction("GestionPromocion");
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