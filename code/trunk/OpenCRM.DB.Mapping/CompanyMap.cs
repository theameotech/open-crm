using OpenCRM.DB.DomainObjects;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenCRM.DB.Mapping
{
    public class CompanyMap : ClassMap<Company>
    {
        public CompanyMap()
        {
            Table("tbl_company");

            Id(x => x.CompanyID).Not.Nullable();
            Map(x => x.CompanyName);
            Map(x => x.BusinessEmail);
            Map(x => x.CompanyType);
            Map(x => x.CompanyAdmin);
            Map(x => x.AdminPassword);
            Map(x => x.CompanyPhone);
            Map(x => x.CompanyAddress);
            Map(x => x.CompanyCity);
            Map(x => x.CompanyState);
            Map(x => x.CompanyZipCode);
            Map(x => x.CompanyCountry);
            Map(x => x.IsVerify);
            Map(x => x.IsActive);
            Map(x => x.IsBlock);
            Map(x => x.IsApproved);
            Map(x => x.CreateTime).Not.Nullable();
            Map(x => x.LastUpdateTime);
            Map(x => x.SystemDate);
            Map(x => x.ServerDate);
            //Map(x => x.AuthToken);


        }
    }
}

