using OpenCRM.Common.DTO;
using OpenCRM.DB.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.BusinessManagers.Interfaces
{
  public  interface IUserManager
    {
      HttpResult AddUser(UserDTO users);

        User GetUser(int userId);

        IList<User> GetAll();

        IList<Roles> GetUserRole(int userId);

        void DeleteUser(int userId);
        void UpdatePassword(User user);
        IList<User> GetAllUsersByCompanyId(int companyId);
      
    }
}
