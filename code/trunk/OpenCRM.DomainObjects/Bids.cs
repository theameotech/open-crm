using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenCRM.DB.DomainObjects
{
    public class Bids : BaseDomainObject
    {
        public virtual int Id { get; set; }
        public virtual int VehicleId { get; set; }
        public virtual int BuyerId { get; set; }
        public virtual string Type { get; set; }
        public virtual string Amount { get; set; }
        public virtual int AuctionId { get; set; }
    }
}