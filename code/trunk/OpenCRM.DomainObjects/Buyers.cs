using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenCRM.DB.DomainObjects
{
    public class Buyers : BaseDomainObject
    {
        public virtual int Id { get; set; }
        public virtual  string Name { get; set; }
        public virtual string BuyerAddress { get; set; }
        public virtual string BuyerEmail { get; set; }
        public virtual string BuyerPhone { get; set; }
        public virtual string ContactFirstName { get; set; }
        public virtual string ContactLastName { get; set; }
        public virtual string Amount { get; set; }

        
    }
}