using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace OpenCRM.DB.DomainObjects
{
    public class DealerShip : BaseDomainObject
    {
        public virtual int Id { get; set; }
        public virtual Guid DealerShipUniqueId { get; set; }
        public virtual string BusinessName { get; set; }
        public virtual string CompanyStructure { get; set; }
        public virtual string IndustryType { get; set; }
        public virtual string BusinessAddress { get; set; }
        public virtual string City { get; set; }
        public virtual string State { get; set; }
        public virtual string ZipCode { get; set; }
        public virtual string BusinessPhone { get; set; }
        public virtual string MonthandYearEstablished { get; set; }
        public virtual string EmailAddress { get; set; }
        public virtual string Password { get; set; }
        public virtual string IsAdmin { get; set; }
        public virtual string CompanyId { get; set; }
        public virtual bool Deleted { get; set; }
        
    }
}
