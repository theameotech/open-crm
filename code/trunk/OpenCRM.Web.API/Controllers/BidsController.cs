using OpenCRM.BusinessManagers.Interfaces;
using OpenCRM.DB.DomainObjects;
using OpenCRM.DB.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OpenCRM.Web.API
{
    [RoutePrefix("api/bids")]
    public class BidsController : ApiController
    {
        private IBidsManager _bidsManager;
        public BidsController(IBidsManager bidsManager)
        {
            _bidsManager = bidsManager;
        }

        [Route("CreateBid")]
        public void CreateBid(Bids bids)
        {
            _bidsManager.CreateBid(bids);
        }

        [Route("All")]
        public IList<Bids> GetAll()
        {
           return _bidsManager.GetAll();
        }
    }


}
