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
    public class BidsHelper : IBidsHelper
    {

         private IBidsRepo _bidsRepo;
         public BidsHelper(IBidsRepo bidsRepo)
        {
            _bidsRepo = bidsRepo;
        }

        public void CreateBid(Bids bids)
        {
            Bids bid = new Bids();
            bid.VehicleId = bids.VehicleId;
            bid.BuyerId = bids.BuyerId;
            bid.Type = bids.Type;
            _bidsRepo.Add(bid);
        }

        public IList<Bids> GetAll()
        {
            return _bidsRepo.FetchAll();
        }
    }
}
