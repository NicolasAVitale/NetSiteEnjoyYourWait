using EnjoyYourWaitNetSite.BusinessLogic;
using System.Web.Mvc;

namespace EnjoyYourWaitNetSite.Controllers
{
    public class HomeController : Controller
    {
        private BSHome bsHome = new BSHome();
        public ActionResult Index()
        {
            //if (SessionHelper.Cliente != null)
            //{

            //} 
            //else
            //{

            //}
            //ViewBag.Message = bsHome
            return View();
        }

        public ActionResult IndexAdmin()
        {
            return View();
        }

        public ActionResult IndexRecepcionista()
        {
            return View();
        }
    }
}