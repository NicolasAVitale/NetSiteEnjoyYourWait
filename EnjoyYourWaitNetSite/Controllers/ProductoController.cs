using EnjoyYourWaitNetSite.BusinessLogic;
using EnjoyYourWaitNetSite.Entities;
using EnjoyYourWaitNetSite.Helper;
using EnjoyYourWaitNetSite.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EnjoyYourWaitNetSite.Controllers
{
    public class ProductoController : Controller
    {
        private BSProducto bsProducto = new BSProducto();
        public async Task<ActionResult> GestionProducto()
        {
            Producto producto = new Producto();
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
                ProductoViewModel model = new ProductoViewModel();
                model.lstProducto = new List<Producto>();
                //Usuario recepcionista = new Usuario();
                producto.IdProducto = 1;
                producto.Nombre = "Hamburguejas al vapor";
                producto.Precio = 120.60;
                producto.Imagen = "hamburguesa.jpg";
                producto.IdTipo = 1;
                model.lstProducto.Add(producto);
                return View("GestionProducto", model);
            }
            catch (Exception)
            {
                TempData["SuccessState"] = "LOAD_FAILED";
                return View("RegistroProducto", producto);
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
                Nombre = producto.Nombre,
                Precio = producto.Precio,
                Imagen = null
            };
            return View("ModificarProducto", model);
        }

        public async Task<ActionResult> AñadirProducto(AddProductoViewModel producto)
        {
            try
            {
                ViewBag.Success = null;
                if (ModelState.IsValid)
                {
                    ViewBag.Success = false;
                    //bool result = await bsProducto.CreateProducto(new Producto()
                    //{
                    //    Nombre = producto.Nombre,
                    //    Precio = producto.Precio,
                    //    Imagen = producto.Imagen.FileName,
                    //    IdTipo = producto.IdTipo
                    //});
                    //if (result)
                    //{
                        string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings.Get("ImagesFolder"), Path.GetFileName(producto.Imagen.FileName));
                        producto.Imagen.SaveAs(fullPath);
                        TempData["Success"] = true;
                        return RedirectToAction("GestionProducto");
                    //}
                }
                //if (!difEdad)
                //{
                //    ViewBag.InvalidAge = "La edad minima requerida debe ser al menos 17 años";
                //}

                return View("RegistroProducto", producto);
            }
            catch (Exception)
            {
                TempData["Success"] = false;
                return View("RegistroProducto", producto);
            }
        }

        public async Task<ActionResult> ModificarProducto(int idProducto)
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
                //bool isValid = Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
                //if (isValid)
                //{
                bool result = await bsProducto.UpdateProducto(idProducto);
                if (result)
                {
                    TempData["SuccessState"] = "UPDATE_SUCCESS";
                }
                //}

                return RedirectToAction("GestionProducto");
            }
            catch (Exception)
            {
                TempData["SuccessState"] = "UPDATE_FAILED";
                return RedirectToAction("GestionProducto");
            }
        }

        public async Task<ActionResult> EliminarProducto(int idProducto)
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
                bool result = await bsProducto.DeleteProducto(idProducto);
                if (result)
                {
                    TempData["SuccessState"] = "DELETE_SUCCESS";
                }
                return RedirectToAction("GestionProducto");
            }
            catch (Exception)
            {
                TempData["SuccessState"] = "DELETE_FAILED";
                return RedirectToAction("GestionProducto");
            }
        }

        public async Task<ActionResult> ObtenerTiposProducto()
        {
            //List<TipoProducto> lista = await bsProducto.ObtenerTiposProducto();
            List<TipoProducto> lista = new List<TipoProducto>();
            lista.Add(new TipoProducto { IdTipo = 1, Descripcion = "Comida" });
            lista.Add(new TipoProducto { IdTipo = 2, Descripcion = "Bebida" });
            return Json(lista);
        }


    }
}