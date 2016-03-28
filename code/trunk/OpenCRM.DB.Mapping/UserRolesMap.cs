using OpenCRM.DB.DomainObjects;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenCRM.DB.Mapping
{
    public class UserRolesMap : ClassMap<UserRoles>
    {
        public UserRolesMap()
        {
            Table("userroles");

            Id(x => x.Id).Not.Nullable().GeneratedBy.Native(builder => builder.AddParam("sequence", "SEQ_AUTO_INCREMENT"));
            Map(x => x.UserId).Not.Nullable();
            Map(x => x.RoleId).Not.Nullable();
            Map(x => x.CreateTime).Not.Nullable();
            Map(x => x.LastUpdateTime);
          
        }
    }
}