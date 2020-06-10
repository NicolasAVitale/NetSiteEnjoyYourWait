using EnjoyYourWaitNetSite.BusinessLogic;
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

        //public void IngresarAFila()
        //{
        //    bsFila.IngresarAFila();
        //}

        public ActionResult IngresarAFila()
        {
            bsFila.IngresarAFila();

            return RedirectToAction("Index", "Home");
        }
    }
}