using OpenCRM.DB.DomainObjects;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenCRM.DB.Mapping
{
    public class MessageMap : ClassMap<Message>
    {
        public MessageMap()
        {
            Table("message");
            Id(x => x.MessageID).Not.Nullable();
            Map(x => x.CompanyID).Not.Nullable();
            Map(x => x.UserID).Not.Nullable();
            Map(x => x.ToUserID).Not.Nullable();
            Map(x => x.MessageBody).Not.Nullable();
            //Map(x => x.TimeStamp).Not.Nullable();
            Map(x => x.CreateTime).Not.Nullable();
            Map(x => x.LastUpdateTime);
            Map(x => x.UserName);
            Map(x => x.ToUserName);

        }
    }
}

