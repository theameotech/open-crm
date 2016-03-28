using AC.Dto;
using AC.Filters;
using AC.Models;
using AC.Repository;
using CsvHelper;
using OpenFinance.DomainObjects;
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

namespace OpenCRM.Web.Controllers
{
    [RoutePrefix("api/dealership")]
    [AuthorizedUser]
    public class DealerShipController : ApiController
    {
        private IDealerShipRepo _dealerShipRepo;
        public DealerShipController(IDealerShipRepo dealerShipRepo)
        {
            _dealerShipRepo = dealerShipRepo;
        }

        [Route("adddealership")]
        [AllowAnonymous]
        public Guid AddDealerShip(DealerShip dealerShip)
        {
            try
            {
                dealerShip.DealerShipUniqueId = Guid.NewGuid();
                dealerShip.Password = string.Join("", MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(dealerShip.Password)).Select(s => s.ToString("x2")));
                _dealerShipRepo.Add(dealerShip);
                return dealerShip.DealerShipUniqueId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
