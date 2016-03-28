using OpenCRM.BizLogic.Helpers.Interfaces;
using OpenCRM.BusinessManagers.Interfaces;
using OpenCRM.DB.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.BusinessManagers.Impl
{
  public  class RolesManager : IRolesManager
    {
      private IRolesHelper _rolesHelper;
      public RolesManager(IRolesHelper rolesHelper) 
      {
          _rolesHelper = rolesHelper;
       
      }
        public IList<Roles> GetAll()
        {
            return _rolesHelper.GetAll();
        }
    }
}
