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
    [RoutePrefix("api/lookup")]
    public class CountryController : ApiController
    {
        private ILookupManager _lookupManager;
        public CountryController(ILookupManager lookupManager)
        {
            _lookupManager = lookupManager;
        }

        [Route("GetAll")]
        public IList<Country> GetAll()
        {
           return _lookupManager.GetAll();
        }
    }


}
