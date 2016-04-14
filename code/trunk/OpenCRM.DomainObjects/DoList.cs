using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenCRM.DB.DomainObjects
{
    public class DoList : BaseDomainObject
    {
        public virtual int DoListID { get; set; }
        public virtual int UserID { get; set; }
        public virtual int CompanyID { get; set; }
        public virtual string ListPriority { get; set; }
        public virtual string ListMessage { get; set; }
        public virtual string ListCategory { get; set; }
        public virtual DateTime TargetDate { get; set; }
        public virtual bool IsRead { get; set; }
        public virtual bool IsDelete { get; set; }
        public virtual string IsActive { get; set; }
        public virtual string IsAcheived { get; set; }
        public virtual string SystemDate { get; set; }
        public virtual string ServerDate { get; set; }
    }
}