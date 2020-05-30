using Entities;
using System.Web;

namespace EnjoyYourWaitNetSite.Helper
{
    public class SessionHelper
    {

        public static Cliente Cliente
        {
            get
            {
                return (Cliente)HttpContext.Current.Session["Cliente"];
            }
            set
            {
                HttpContext.Current.Session["Cliente"] = value;
            }
        }

        public static Recepcionista Recepcionista
        {
            get
            {
                return (Recepcionista)HttpContext.Current.Session["Recepcionista"];
            }
            set
            {
                HttpContext.Current.Session["Recepcionista"] = value;
            }
        }

        public static Administrador Administrador
        {
            get
            {
                return (Administrador)HttpContext.Current.Session["Administrador"];
            }
            set
            {
                HttpContext.Current.Session["Administrador"] = value;
            }
        }

    }
}