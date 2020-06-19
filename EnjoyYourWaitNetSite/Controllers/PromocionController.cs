using EnjoyYourWaitNetSite.BusinessLogic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EnjoyYourWaitNetSite.Controllers
{
    public class PromocionController : Controller
    {
        private BSHome bsHome = new BSHome();

        public ActionResult Promociones()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AgregarProducto()
        {
            return View();
        }

        public ActionResult ModificarProducto()
        {
            return View();
        }

        public ActionResult EliminarProducto()
        {
            return View();
        }
    }
}