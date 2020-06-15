using EnjoyYourWaitNetSite.BusinessLogic;
using System;
using System.Text.RegularExpressions;
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
    }
}