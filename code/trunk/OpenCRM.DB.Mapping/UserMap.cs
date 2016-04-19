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
            Table("tbl_users");

            Id(x => x.Id).Not.Nullable();
            Map(x => x.UserName).Not.Nullable();
            Map(x => x.UserPassword).Not.Nullable();
            Map(x => x.AuthToken);
            Map(x => x.UserEmail).Not.Nullable(); 
            Map(x => x.UserPhone).Not.Nullable(); 
            Map(x => x.CreateTime).Not.Nullable();
            Map(x => x.LastUpdateTime);
            Map(x => x.FirstName).Not.Nullable(); 
            Map(x => x.LastName).Not.Nullable(); 
            Map(x => x.UserAddress);
            Map(x => x.UserAlternateAddress);
            Map(x => x.UserCity);
            Map(x => x.UserState);
            Map(x => x.UserZipCode);
            Map(x => x.UserCountry);
            Map(x => x.UserOfficPhone);
            Map(x => x.UserOfficePhoneExt);
            Map(x => x.IsActive).Not.Nullable(); 
            Map(x => x.IsVerify);
            Map(x => x.Deleted);
            Map(x => x.UserPrivilege).Not.Nullable(); 
            Map(x => x.Gender); 
            Map(x => x.Isblock).Not.Nullable(); 
            Map(x => x.CompanyID).Not.Nullable();

        }
    }
}

