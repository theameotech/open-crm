using OpenCRM.Common.DTO;
using OpenCRM.DB.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.BizLogic.Helpers.Interfaces
{
  public interface ILoginHelper
    {
      void Logout(User user);
      LoginResult login(User user);
      bool IsAdmin(string username);

    }
}
