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
            //Si no existe el token se abre el Login
            if (SessionHelper.Token == null)
            {
                UserAuthenticationViewModel user = new UserAuthenticationViewModel();
                return View("Login", user);
            }

            return RedirectToAction("Index",
                "Home");
        }

        public async Task<ActionResult> LoginAuth(UserAuthenticationViewModel userModel)
        {
            try
            {
                ViewBag.SuccessState = null;
                if (ModelState.IsValid)
                {
                    ViewBag.SuccessState = "LOGIN_FAILED";

                    Usuario user = new Usuario
                    {
                        Email = userModel.Email,
                        Contrasena = userModel.Contrasena
                    };

                    if (!(await bsLogin.GetAuthToken(user) is object[] dataConnection) || dataConnection.Length != 2 || dataConnection[0] == null)
                        throw new AuthException("Ha ocurrido un error con sus credenciales");

                    SessionHelper.Token = dataConnection[0].ToString();
                    Usuario userAuth = (Usuario)dataConnection[1];
                    if (userAuth.IdRol == 1)
                    {
                        SessionHelper.Administrador = new Administrador
                        {
                            IdUsuario = userAuth.IdUsuario,
                            Dni = userAuth.Dni,
                            Nombre = userAuth.Nombre,
                            Apellido = userAuth.Apellido,
                            Email = userAuth.Email,
                            FechaNacimiento = userAuth.FechaNacimiento,
                            Contrasena = userAuth.Contrasena,
                            IdRol = userAuth.IdRol
                        };
                    } 
                    else
                    {
                        SessionHelper.Recepcionista = new Recepcionista
                        {
                            IdUsuario = userAuth.IdUsuario,
                            Dni = userAuth.Dni,
                            Nombre = userAuth.Nombre,
                            Apellido = userAuth.Apellido,
                            Email = userAuth.Email,
                            FechaNacimiento = userAuth.FechaNacimiento,
                            Contrasena = userAuth.Contrasena,
                            IdRol = userAuth.IdRol
                        };
                    }
                    return RedirectToAction("Index",
                        "Home");
                }
                return View("Login",
                    userModel);
            }
            catch (Exception)
            {
                ViewBag.SuccessState = "TOKEN_FAILED";
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