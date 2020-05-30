using System.Threading.Tasks;

namespace EnjoyYourWaitNetSite.BusinessLogic
{
    public class BSHome : BSBase
    {
        public BSHome()
        {
        }

        public async Task<string> GetHolaMundoAsync()
        {
            return await dataAccess.GetHolaMundo();
        }
    }
}
