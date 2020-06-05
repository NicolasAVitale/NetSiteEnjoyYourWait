using EnjoyYourWaitNetSite.Entities;
using System;
using System.Collections.Generic;
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

        public async Task<bool> UpdateRecepcionista(int dni, string email)
        {
            return await dataAccess.UpdateRecepcionista(dni, email);
        }

        public Task<List<Usuario>> GetAllRecepcionistas()
        {
            return await dataAccess.GetAllRecepcionistas();
        }
    }
}
