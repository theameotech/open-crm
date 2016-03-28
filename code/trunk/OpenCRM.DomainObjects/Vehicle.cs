using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenCRM.DB.DomainObjects
{
    public class Vehicle : BaseDomainObject
    {
        public virtual int Id { get; set; }

        public virtual int AuctionId { get; set; }
        public virtual string Name { get; set; }

        public virtual string Model { get; set; }

        public virtual long OdoMeter { get; set; }
        public virtual string Vin { get; set; }

    }
}