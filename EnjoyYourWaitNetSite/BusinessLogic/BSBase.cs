using EnjoyYourWaitNetSite.DataAccess;

namespace EnjoyYourWaitNetSite.BusinessLogic
{
    public class BSBase
    {
        protected readonly DataAccessEYW dataAccess;

        public BSBase()
        {
            dataAccess = new DataAccessEYW();
        }
    }
}
