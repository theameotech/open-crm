using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenCRM.DB.DomainObjects
{
    public class BaseDomainObject
    {
        public virtual DateTime CreateTime { get; set; }
        public virtual DateTime? LastUpdateTime { get; set; }
        
    }
}