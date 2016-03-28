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
    [RoutePrefix("api/country")]
    public class CountryController : ApiController
    {
        private IRepository<Country> _countryRepo;
        public CountryController(IRepository<Country> countryRepo)
        {
            _countryRepo = countryRepo;
        }

        [Route("GetAll")]
        public IList<Country> GetAll()
        {
            return _countryRepo.FetchAll();
        }
    }


}
