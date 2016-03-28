using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenCRM.DB.DomainObjects
{
    public class Message : BaseDomainObject
    {
        public virtual int MessageID { get; set; }
        public virtual int CompanyID { get; set; }
        public virtual int UserID { get; set; }
        public virtual int ToUserID { get; set; }
        public virtual string MessageBody { get; set; }
        public virtual string UserName { get; set; }
        public virtual string ToUserName { get; set; }
        //public virtual DateTime TimeStamp { get; set; }
 
        
    }
}