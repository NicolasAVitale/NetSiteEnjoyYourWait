using EnjoyYourWaitNetSite.Entities;
using System.Threading.Tasks;

namespace EnjoyYourWaitNetSite.BusinessLogic
{
    public class BSRecepcionista : BSBase
    {
        public BSRecepcionista()
        {
        }

        public async Task<bool> CreateRecepcionista(Usuario recepcionista)
        {
            return await dataAccess.CreateRecepcionista(recepcionista);
        }
        public async Task<bool> DeleteRecepcionista(int dni)
        {
            return await dataAccess.DeleteRecepcionista(dni);
        }
    }
}
