using OpenCRM.DB.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using OpenCRM.Common.DTO;

namespace OpenCRM.BizLogic.Helpers.Interfaces
{
  public  interface IAuctionHelper
    {
      void CreateAuction(Auction auct);
      IList<Auction> GetAll();

      HttpResult Post(AuctionBids auction);
    }
}
