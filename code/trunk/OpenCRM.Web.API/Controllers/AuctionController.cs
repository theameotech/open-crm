using OpenCRM.BizLogic.Helpers.Interfaces;
using OpenCRM.BusinessManagers.Interfaces;
using OpenCRM.Common.DTO;
using OpenCRM.DB.DomainObjects;
using OpenCRM.DB.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;


namespace OpenCRM.Web.API
{
    [RoutePrefix("api/auction")]
    public class AuctionController : ApiController
    {

        private IAuctionManager _auctionManager;

        public AuctionController(IAuctionManager auctionManager)
        {
            _auctionManager = auctionManager;
            

        }

        [Route("CreateAuction")]
        public void CreateAuction(Auction auct)
        {
            _auctionManager.CreateAuction(auct);
        }

        [Route("All")]
        public IList<Auction> GetAll()
        {
            return _auctionManager.GetAll();
        }

        [Route("PostBids")]
        public HttpResult Post(AuctionBids auction)
        {
            return _auctionManager .Post(auction);
           
        }
    }

}
