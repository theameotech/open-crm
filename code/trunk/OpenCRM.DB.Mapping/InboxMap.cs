using FluentNHibernate.Mapping;
using OpenCRM.DB.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.DB.Mapping
{
    public class InboxMap : ClassMap<Inbox>
    {
        public InboxMap()
        {
            Table("tbl_emails");

            Id(x => x.StatusID).Not.Nullable();
            Map(x => x.EmailID).Not.Nullable();
            Map(x => x.CompanyID).Not.Nullable();
            Map(x => x.UserID).Not.Nullable();
            Map(x => x.IPAddress).Not.Nullable();
            Map(x => x.IsAttachment).Not.Nullable();
            Map(x => x.IsRead).Not.Nullable();
            Map(x => x.IsReply).Not.Nullable();
            Map(x => x.IsTrash).Not.Nullable();
            Map(x => x.IsDraft).Not.Nullable();
            Map(x => x.IsFlagged).Not.Nullable();
            Map(x => x.IsForwarded).Not.Nullable();
            Map(x => x.ServerDate).Not.Nullable();
            Map(x => x.SystemDate).Not.Nullable();
            Map(x => x.EmailSubject).Not.Nullable();
            Map(x => x.EmailRecipient).Not.Nullable();
            Map(x => x.EmailSender).Not.Nullable();
            Map(x => x.EmailContent).Not.Nullable();
            Map(x => x.AttachmentName).Not.Nullable();
            Map(x => x.AttachmentType).Not.Nullable();
            Map(x => x.CreateTime).Not.Nullable();
            Map(x => x.LastUpdateTime);
            Map(x => x.ToUserName).Not.Nullable();
            Map(x => x.FromUserName).Not.Nullable();
        }


    }
}
