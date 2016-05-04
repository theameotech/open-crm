using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenCRM.DB.DomainObjects
{
    public class EmailTemplate : BaseDomainObject
    {
        public virtual int EmailTemplateID { get; set; }
        public virtual string EmailTemplateName { get; set; }
        public virtual string EmailTemplateBody { get; set; }
        public virtual DateTime CreateDate { get; set; }
        public virtual DateTime ModifiedDate { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual bool IsDelete { get; set; }
        public virtual DateTime SystemDate { get; set; }
        public virtual DateTime ServerDate { get; set; }

    }
}
