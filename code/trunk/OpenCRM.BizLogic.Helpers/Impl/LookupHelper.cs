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
   public class LookupHelper: ILookupHelper
    {

        private ICountryRepo _countryRepo;
        public LookupHelper(ICountryRepo countryRepo)
        {
            _countryRepo = countryRepo;
        } 
        public IList<Country> GetAll()
        {
            return _countryRepo.FetchAll();
        }
    }
}
