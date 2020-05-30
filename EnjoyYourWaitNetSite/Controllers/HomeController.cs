using EnjoyYourWaitNetSite.BusinessLogic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EnjoyYourWaitNetSite.Controllers
{
    public class HomeController : Controller
    {
        private BSHome bsHome = new BSHome();
        public async Task<ActionResult> Index()
        {
            //if (SessionHelper.Cliente != null)
            //{

            //} 
            //else
            //{

            //}
            string msg = await bsHome.GetHolaMundoAsync();
            ViewBag.Message = msg;
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