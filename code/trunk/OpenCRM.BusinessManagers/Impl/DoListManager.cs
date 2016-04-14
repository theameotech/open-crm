using OpenCRM.BizLogic.Helpers.Interfaces;
using OpenCRM.BusinessManagers.Interfaces;
using OpenCRM.Common.DTO;
using OpenCRM.DB.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace OpenCRM.BusinessManagers.Impl
{
    public class DoListManager : IDoListManager
    {
        private IDoListHelper _doListHelper;
        public DoListManager(IDoListHelper doListHelper)
        {
            _doListHelper = doListHelper;
        }
       

      
    }
}