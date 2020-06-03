using System.Threading.Tasks;

namespace EnjoyYourWaitNetSite.BusinessLogic
{
    public class BSRecepcionista : BSBase
    {
        public BSRecepcionista()
        {
        }

        public async Task<bool> CreateRecepcionista(Entities.Recepcionista recepcionista)
        {
            return await dataAccess.CreateRecepcionista();
        }
    }
}
