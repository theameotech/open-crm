using OpenCRM.BizLogic.Helpers.Interfaces;
using OpenCRM.BusinessManagers.Interfaces;
using OpenCRM.Common.DTO;
using OpenCRM.DB.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace OpenCRM.BusinessManagers.Impl
{
    public class UserManager : IUserManager
    {
        private IUserHelper _userHelper;
        public UserManager(IUserHelper userHelper)
        {
            _userHelper = userHelper;
        }
        public HttpResult AddUser(UserDTO users)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                     _userHelper.AddUser(users);
                     scope.Complete();
                }
                catch (AlreadyExistException aex)
                {
                    return new HttpResult(aex.Message, false);

                }


                catch (Exception ex)
                {
                    return new HttpResult(ex.Message, false);
                  
                }
            }
            return new HttpResult("Created Successfully!", true);
        }

        public User GetUser(int userId)
        {
            return _userHelper.GetUser(userId);
        }

        public IList<User> GetAll()
        {
            return _userHelper.GetAll();
        }

        public IList<Roles> GetUserRole(int userId)
        {
            return _userHelper.GetUserRole(userId);
        }

        public void DeleteUser(int userId)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    _userHelper.DeleteUser(userId);
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


      public  void UpdatePassword(User user)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    _userHelper.UpdatePassword(user);
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
      public IList<User> GetAllUsersByCompanyId(int companyId)
      {
          return _userHelper.GetAllUsersByCompanyId(companyId);
      }
    }
}