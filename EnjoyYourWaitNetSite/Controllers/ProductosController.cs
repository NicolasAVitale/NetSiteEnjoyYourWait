using EnjoyYourWaitNetSite.BusinessLogic;
using EnjoyYourWaitNetSite.Helper;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EnjoyYourWaitNetSite.Controllers
{
    public class ProductosController : Controller
    {
        private BSHome bsHome = new BSHome();
        public async Task<ActionResult> Index()
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