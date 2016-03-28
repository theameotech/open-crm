using OpenCRM.DB.DomainObjects;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenCRM.DB.Mapping
{
    public class LogedInUsersMap : ClassMap<LogedInUsers>
    {
        public LogedInUsersMap()
        {
            Table("logedinusers");
            Id(x => x.LoggedInUserID).Not.Nullable();
            Id(x => x.CompanyID).Not.Nullable();
            Id(x => x.UserID).Not.Nullable();
        }
    }
}

