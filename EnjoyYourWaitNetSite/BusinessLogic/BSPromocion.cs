using EnjoyYourWaitNetSite.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace EnjoyYourWaitNetSite.BusinessLogic
{
    public class BSPromocion : BSBase
    {
        public async Task<List<Promocion>> GetAllPromociones()
        {
            return await dataAccess.GetAllPromociones();
        }

        public async Task<bool> CreatePromocion(Promocion promocion)
        {
            return await dataAccess.CreatePromocion(promocion);
        }

        public async Task<bool> DisablePromocion(int idPromocion)
        {
            return await dataAccess.DisablePromocion(idPromocion);
        }

        public async Task<bool> EnablePromocion(int idPromocion)
        {
            return await dataAccess.EnablePromocion(idPromocion);
        }
    }
}