using CsvHelper;
using OpenCRM.BusinessManagers.Interfaces;
using OpenCRM.DB.DomainObjects;
using OpenCRM.DB.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Transactions;
using System.Web;
using System.Web.Http;

namespace OpenCRM.Web.API
{
    [RoutePrefix("api/dealership")]
    [AuthorizedUser]
    public class DealerShipController : ApiController
    {
        private IDealerShipManager _dealerShipManager;
        public DealerShipController(IDealerShipManager dealerShipManager)
        {
            _dealerShipManager = dealerShipManager;
        }

        [Route("adddealership")]
        [AllowAnonymous]
        public Guid AddDealerShip(DealerShip dealerShip)
        {
          return _dealerShipManager.AddDealerShip(dealerShip);
        }
    }
}
