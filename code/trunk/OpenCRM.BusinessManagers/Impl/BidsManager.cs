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
    public class BidsManager : IBidsManager
    {

        private IBidsHelper _bidsHelper;
        public BidsManager(IBidsHelper bidsHelper)
        {
            _bidsHelper = bidsHelper;
        }

        public void CreateBid(Bids bids)
        {
            _bidsHelper.CreateBid(bids);
        }

        public IList<Bids> GetAll()
        {
            return _bidsHelper.GetAll();
        }
    }
}
