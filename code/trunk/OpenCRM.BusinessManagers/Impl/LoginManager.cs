using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCRM.Common.DTO;
using OpenCRM.DB.DomainObjects;
using OpenCRM.BusinessManagers.Interfaces;
using OpenCRM.BizLogic.Helpers.Interfaces;
using System.Transactions;


namespace OpenCRM.BusinessManagers.Impl
{
    public class LoginManager : ILoginManager
    {
        private ILoginHelper _logiHelper;
        public LoginManager(ILoginHelper logiHelper)
        {
            _logiHelper = logiHelper;
        }

        public void Logout(User user)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                _logiHelper.Logout(user);
                scope.Complete();
            }
        }

        public LoginResult login(User user)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {

                    return _logiHelper.login(user);
                }
                catch (Exception)
                {
                    scope.Complete();
                }
                return _logiHelper.login(user);
            }


        }

        public bool IsAdmin(string username)
        {
            return _logiHelper.IsAdmin(username);
        }
    }
}
