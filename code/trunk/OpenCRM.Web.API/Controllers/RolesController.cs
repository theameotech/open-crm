using OpenCRM.BusinessManagers.Interfaces;
using OpenCRM.DB.DomainObjects;
using OpenCRM.DB.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OpenCRM.Web.API
{
    [RoutePrefix("api/roles")]
    public class RolesController : ApiController
    {
        private IRolesManager _rolesManager;
        public RolesController(IRolesManager rolesManager)
        {
            _rolesManager = rolesManager;
        }

        [Route("All")]
        public IList<Roles> GetAll()
        {
            return _rolesManager.GetAll();
        }

    }
}
