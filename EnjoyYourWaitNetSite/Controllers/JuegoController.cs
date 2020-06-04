using EnjoyYourWaitNetSite.BusinessLogic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EnjoyYourWaitNetSite.Controllers
{
    public class JuegoController : Controller
    {
        private BSHome bsHome = new BSHome();
        public ActionResult Juegos()
        {
            return View();
        }

        public ActionResult Memotest()
        {
            return View();
        }
        public ActionResult Ruleta()
        {
            return View();
        }
    }
}