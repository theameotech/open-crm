using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenCRM.Common.DTO;
using OpenCRM.DB.DomainObjects;

namespace OpenCRM.BusinessManagers.Interfaces
{
   public interface IAuctionManager
    {
        void CreateAuction(Auction auct);
        IList<Auction> GetAll();

        HttpResult Post(AuctionBids auction);

    }
}
