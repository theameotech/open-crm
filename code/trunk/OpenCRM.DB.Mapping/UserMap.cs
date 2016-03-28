using OpenCRM.DB.DomainObjects;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenCRM.DB.Mapping
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Table("user");

            Id(x => x.Id).Not.Nullable();
            Map(x => x.UserName).Not.Nullable();
            Map(x => x.Password).Not.Nullable();
            Map(x => x.AuthToken);
            Map(x => x.Email);
            Map(x => x.Phone);
            Map(x => x.CreateTime).Not.Nullable();
            Map(x => x.LastUpdateTime);
            Map(x => x.Deleted);
            Map(x => x.FirstName);
            Map(x => x.LastName);
            Map(x => x.Office);
            Map(x => x.StreetAddress1);
            Map(x => x.StreetAddress2);
            Map(x => x.City);
            Map(x => x.State);
            Map(x => x.PostalCode);
            Map(x => x.Country);
            Map(x => x.EXT);
            Map(x => x.CompanyId).Not.Nullable();



        }
    }
}

