using EnjoyYourWaitNetSite.Entities;
using System;
using System.Threading.Tasks;

namespace EnjoyYourWaitNetSite.BusinessLogic
{
    public class BSLogin : BSBase
    {
        public BSLogin()
        {
        }

        public async Task<Usuario> Login(UserLoginEntity userCredentials)
        {
            return await dataAccess.Login(userCredentials);
        }
    }
}
