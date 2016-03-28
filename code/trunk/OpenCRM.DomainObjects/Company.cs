using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenCRM.DB.DomainObjects
{
    public class Company : BaseDomainObject
    {
        public virtual int CompanyID { get; set; }
        public virtual string CompanyName { get; set; }
      

    }
}