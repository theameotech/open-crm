using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenCRM.DB.DomainObjects
{
    public class LogedInUsers : BaseDomainObject
    {
        public virtual int LoggedInUserID { get; set; }
        public virtual int UserID { get; set; }
        public virtual int CompanyID { get; set; }
    }
}