using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.DB.DomainObjects
{
    public class Inbox : BaseDomainObject
    {
        public virtual int StatusID { get; set; }
        public virtual int EmailID { get; set; }
        public virtual int UserID { get; set; }
        public virtual int CompanyID { get; set; }
        public virtual bool IsRead { get; set; }
        public virtual bool IsDraft { get; set; }
        public virtual bool IsTrash { get; set; }
        public virtual bool IsReply { get; set; }
        public virtual bool IsFlagged { get; set; }
        public virtual bool IsForwarded { get; set; }
        public virtual bool IsAttachment { get; set; }
        public virtual DateTime SystemDate { get; set; }
        public virtual DateTime ServerDate { get; set; }
        public virtual string EmailSubject { get; set; }
        public virtual string EmailContent { get; set; }
        public virtual string EmailSender { get; set; }
        public virtual string EmailRecipient { get; set; }
        public virtual byte[] AttachmentName { get; set; }
         public virtual string AttachmentType { get; set; }
        public virtual string IPAddress { get; set; }
        public virtual string ToUserName { get; set; }
        public virtual string FromUserName { get; set; }

    }
}
