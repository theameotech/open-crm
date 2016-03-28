using AC.Dto;
using AC.Models;
using AC.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OpenCRM.Web.Controllers
{
    [RoutePrefix("api/roles")]
    public class RolesController : ApiController
    {
        private IRepository<Roles> _rolesRepo;
        public RolesController(IRepository<Roles> rolesRepo)
        {
            _rolesRepo = rolesRepo;
        }

        [Route("All")]
        public IList<Roles> GetAll()
        {
            return _rolesRepo.FetchAll();
        }

    }
}
