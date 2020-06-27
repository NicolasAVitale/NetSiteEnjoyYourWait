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
        public async Task<bool> DisableRecepcionista(int dni)
        {
            return await dataAccess.DisableRecepcionista(dni);
        }

        public async Task<bool> EnableRecepcionista(int id)
        {
            return await dataAccess.EnableRecepcionista(id);
        }

        public async Task<bool> UpdateRecepcionista(int id, Usuario user)
        {
            return await dataAccess.UpdateRecepcionista(id, user);
        }

        public async Task<List<Usuario>> GetAllRecepcionistas()
        {
            return await dataAccess.GetAllRecepcionistas();
        }
    }
}
