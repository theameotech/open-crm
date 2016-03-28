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
    [RoutePrefix("api/bids")]
    public class BidsController : ApiController
    {
        private IRepository<Bids> _bidsRepo;
        public BidsController(IRepository<Bids> bidsRepo)
        {
            _bidsRepo = bidsRepo;
        }

        [Route("CreateBid")]
        public void CreateBid(Bids bids)
        {
            Bids bid = new Bids();
            bid.VehicleId = bids.VehicleId;
            bid.BuyerId = bids.BuyerId;
            bid.Type = bids.Type;
            _bidsRepo.Add(bid);
        }

        [Route("All")]
        public IList<Bids> GetAll()
        {
            return _bidsRepo.FetchAll();
        }
    }


}
