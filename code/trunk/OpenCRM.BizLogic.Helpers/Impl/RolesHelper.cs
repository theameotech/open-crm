using OpenCRM.BizLogic.Helpers.Interfaces;
using OpenCRM.DB.DomainObjects;
using OpenCRM.DB.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.BizLogic.Helpers.Impl
{
  public class RolesHelper : IRolesHelper
    {
        private IRoleRepo _rolesRepo;
        public RolesHelper(IRoleRepo rolesRepo)
        {
            _rolesRepo = rolesRepo;
        }
        public IList<Roles> GetAll()
        {
            return _rolesRepo.FetchAll();
        }
    }
}
