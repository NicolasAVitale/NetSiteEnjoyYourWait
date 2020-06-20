using EnjoyYourWaitNetSite.BusinessLogic;
using EnjoyYourWaitNetSite.Entities;
using EnjoyYourWaitNetSite.Exceptions;
using EnjoyYourWaitNetSite.Helper;
using EnjoyYourWaitNetSite.Models;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EnjoyYourWaitNetSite.Controllers
{
    public class LoginController : Controller
    {
        private BSLogin bsLogin = new BSLogin();
        public ActionResult Index()
        {
            UserLoginViewModel user = new UserLoginViewModel();
            return View("Login", user);
        }

        public async Task<ActionResult> LoginAuth(UserLoginViewModel userModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TempData["SuccessState"] = "LOGIN_FAILED";
                    ViewBag.SuccessState = TempData["SuccessState"];

                    UserLoginEntity userCredentials = new UserLoginEntity
                    {
                        email = userModel.Email,
                        contrasena = userModel.Contrasena
                    };

                    Usuario userLogin = await bsLogin.Login(userCredentials);
                    if(userLogin != null)
                    {
                        if (userLogin.idRol == 1)
                        {
                            SessionHelper.Administrador = new Administrador
                            {
                                idUsuario = userLogin.idUsuario,
                                dni = userLogin.dni,
                                nombre = userLogin.nombre,
                                apellido = userLogin.apellido,
                                email = userLogin.email,
                                fechaNacimiento = userLogin.fechaNacimiento,
                                contrasena = userLogin.contrasena,
                                idRol = userLogin.idRol
                            };
                        }
                        else
                        {
                            SessionHelper.Recepcionista = new Recepcionista
                            {
                                idUsuario = userLogin.idUsuario,
                                dni = userLogin.dni,
                                nombre = userLogin.nombre,
                                apellido = userLogin.apellido,
                                email = userLogin.email,
                                fechaNacimiento = userLogin.fechaNacimiento,
                                contrasena = userLogin.contrasena,
                                idRol = userLogin.idRol
                            };
                        }

                        TempData["SuccessState"] = "LOGIN_SUCCESS";
                        ViewBag.SuccessState = TempData["SuccessState"];

                        return RedirectToAction("Index",
                            "Home");
                    }
                }
                return View("Login",
                    userModel);
            }
            catch (Exception)
            {
                TempData["SuccessState"] = "UNEXPECTED_ERROR";
                ViewBag.SuccessState = TempData["SuccessState"];
                return View("Login",
                    userModel);
            }
        }

        public ActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index",
                "Login");
        }
    }
}