using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenCRM.DB.DomainObjects
{
    public class Country : BaseDomainObject
    {
        public virtual int Id { get; set; }
        public virtual string ISO { get; set; }
        public virtual string Name { get; set; }

        public virtual string NiceName { get; set; }
        public virtual string ISO3 { get; set; }
        public virtual int NumCode { get; set; }
        public virtual int PhoneCode { get; set; }
    }
}