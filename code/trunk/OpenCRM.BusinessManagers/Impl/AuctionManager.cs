using OpenCRM.BizLogic.Helpers.Interfaces;
using OpenCRM.BusinessManagers.Interfaces;
using OpenCRM.Common.DTO;
using OpenCRM.DB.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.BusinessManagers.Impl
{
    public class AuctionManager : IAuctionManager
    {
         private IAuctionHelper _auctionHelper;

         public AuctionManager(IAuctionHelper auctionHelper)
        {
            _auctionHelper = auctionHelper;
            

        }


        public void CreateAuction(Auction auct)
        {
            _auctionHelper.CreateAuction(auct);
        }

        public IList<Auction> GetAll()
        {
            return _auctionHelper.GetAll();
        }

        public HttpResult Post(AuctionBids auction)
        {
            return _auctionHelper.Post(auction);
        }
    }
}
