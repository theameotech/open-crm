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
   public class LookupManager:ILookupManager
    {
       private ILookupHelper _lookupHelper;
       public LookupManager(ILookupHelper lookupHelper)
       {
           _lookupHelper = lookupHelper;

       }


        public IList<Country> GetAll()
        {

           return _lookupHelper.GetAll();
        }
    }
}
