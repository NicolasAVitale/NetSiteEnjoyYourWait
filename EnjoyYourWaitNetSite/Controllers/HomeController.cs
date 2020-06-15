using EnjoyYourWaitNetSite.BusinessLogic;
using System.Web.Mvc;

namespace EnjoyYourWaitNetSite.Controllers
{
    public class HomeController : Controller
    {
        private BSHome bsHome = new BSHome();
        public ActionResult Index()
        {
            ViewBag.SuccessState = TempData["SuccessState"];
            return View();
        }
    }
}