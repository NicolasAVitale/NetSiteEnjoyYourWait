using EnjoyYourWaitNetSite.BusinessLogic;
using EnjoyYourWaitNetSite.Entities;
using EnjoyYourWaitNetSite.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EnjoyYourWaitNetSite.Controllers
{
    public class ProductoController : Controller
    {
        private BSProducto bsProducto = new BSProducto();

        public async Task<ActionResult> Menu()
        {
            MenuViewModel model = new MenuViewModel();
            try
            {
                model.ProductosMenu = new List<Producto>();

                List<Producto> productosActivos = await bsProducto.ObtenerProductosActivos();

                if (productosActivos.Count == 0)
                {
                    TempData["SuccessState"] = "LOAD_NOPRODUCTS";
                    ViewBag.SuccessState = TempData["SuccessState"];
                }
                else
                {
                    foreach (var productoActivo in productosActivos)
                    {
                        model.ProductosMenu.Add(new Producto()
                        {
                            nombre = productoActivo.nombre,
                            imagen = productoActivo.imagen,
                            precio = productoActivo.precio
                        });
                    }
                }

                return View(model);
            }
            catch (Exception)
            {
                TempData["SuccessState"] = "LOAD_FAILED";
                ViewBag.SuccessState = TempData["SuccessState"];
                return View(model);
            }
        }

        public async Task<ActionResult> GestionProducto()
        {
            ProductoViewModel model = new ProductoViewModel();
            model.lstProducto = new List<Producto>();
            try
            {
                ViewBag.SuccessState = TempData["SuccessState"];
                    
                //Cargo lista de productos
                List<Producto> productos = await bsProducto.GetAllProductos();
                model.lstProducto = productos;
                if (productos.Count == 0)
                {
                    TempData["SuccessState"] = "LOAD_NOPRODUCTS";
                }
                return View("GestionProducto", model);
            }
            catch (Exception)
            {
                TempData["SuccessState"] = "LOAD_FAILED";
                return View("GestionProducto", model);
            }
        }

        public ActionResult AbrirRegistroProducto()
        {
            AddProductoViewModel model = new AddProductoViewModel();
            return View("RegistroProducto", model);
        }

        public ActionResult AbrirModificarProducto(Producto producto)
        {
            UpdateProductoViewModel model = new UpdateProductoViewModel
            {
                Nombre = producto.nombre,
                Precio = producto.precio,
                Imagen = null,
                ImagenName = producto.imagen,
                IdProducto = producto.idProducto
            };
            return View("ModificarProducto", model);
        }

        public async Task<ActionResult> AñadirProducto(AddProductoViewModel producto)
        {
            try
            {
                ViewBag.Success = null;
                if (ModelState.IsValid && producto.IdTipo > 0)
                {
                    ViewBag.Success = false;
                    bool result = await bsProducto.CreateProducto(new Producto()
                    {
                        nombre = producto.Nombre,
                        precio = producto.Precio,
                        imagen = producto.Imagen.FileName,
                        idTipo = producto.IdTipo
                    });
                    if (result)
                    {
                        string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings.Get("ImagesFolder"), Path.GetFileName(producto.Imagen.FileName));
                        producto.Imagen.SaveAs(fullPath);
                        TempData["Success"] = true;
                        return RedirectToAction("GestionProducto");
                    }
                }

                return View("RegistroProducto", producto);
            }
            catch (Exception)
            {
                TempData["Success"] = false;
                return View("RegistroProducto", producto);
            }
        }

        public async Task<ActionResult> ModificarProducto(UpdateProductoViewModel producto)
        {
            try
            {
                TempData["SuccessState"] = "UPDATE_FAILED";
                UpdateProductoApiModel productoApi = new UpdateProductoApiModel()
                {
                    nombre = producto.Nombre,
                    precio = producto.Precio,
                    imagen = producto.Imagen == null ? producto.ImagenName : producto.Imagen.FileName
                };
                bool result = await bsProducto.UpdateProducto(producto.IdProducto, productoApi);
                if (result)
                {
                    if(producto.Imagen != null)
                    {
                        string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings.Get("ImagesFolder"), Path.GetFileName(producto.Imagen.FileName));
                        producto.Imagen.SaveAs(fullPath);
                    }
                    TempData["SuccessState"] = "UPDATE_SUCCESS";
                    return RedirectToAction("GestionProducto");
                }

                return RedirectToAction("GestionProducto");
            }
            catch (Exception)
            {
                TempData["SuccessState"] = "UPDATE_FAILED";
                return RedirectToAction("GestionProducto");
            }
        }

        public async Task<ActionResult> DeshabilitarProducto(int idProducto)
        {
            try
            {
                TempData["SuccessState"] = "DISABLE_FAILED";
                bool result = await bsProducto.DisableProducto(idProducto);
                if (result)
                {
                    TempData["SuccessState"] = "DISABLE_SUCCESS";
                }
                return RedirectToAction("GestionProducto");
            }
            catch (Exception)
            {
                TempData["SuccessState"] = "DISABLE_FAILED";
                return RedirectToAction("GestionProducto");
            }
        }

        public async Task<ActionResult> HabilitarProducto(int idProducto)
        {
            try
            {
                TempData["SuccessState"] = "ENABLE_FAILED";
                bool result = await bsProducto.EnableProducto(idProducto);
                if (result)
                {
                    TempData["SuccessState"] = "ENABLE_SUCCESS";
                }
                return RedirectToAction("GestionProducto");
            }
            catch (Exception)
            {
                TempData["SuccessState"] = "ENABLE_FAILED";
                return RedirectToAction("GestionProducto");
            }
        }

        public ActionResult ObtenerTiposProducto()
        {
            List<TipoProducto> lista = new List<TipoProducto>();
            lista.Add(new TipoProducto { IdTipo = 1, Descripcion = "Comida" });
            lista.Add(new TipoProducto { IdTipo = 2, Descripcion = "Bebida" });
            return Json(lista);
        }

    }
}