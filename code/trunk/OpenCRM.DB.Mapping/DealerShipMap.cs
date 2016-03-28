using FluentNHibernate.Mapping;
using OpenCRM.DB.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenFinance.Mapping
{
    public class DealerShipMap : ClassMap<DealerShip>
    {
        public DealerShipMap()
        {
            Table("dealership");
            Id(x => x.Id).Not.Nullable();
            Map(x => x.DealerShipUniqueId);
            Map(x => x.BusinessName);
            Map(x => x.CompanyStructure);
            Map(x => x.IndustryType);
            Map(x => x.BusinessAddress);
            Map(x => x.City);
            Map(x => x.State);
            Map(x => x.ZipCode);
            Map(x => x.BusinessPhone);
            Map(x => x.MonthandYearEstablished);
            Map(x => x.EmailAddress);
            Map(x => x.Password);
            Map(x => x.IsAdmin);
            Map(x => x.CompanyId);
            Map(x => x.Deleted);
            Map(x => x.CreateTime);
            Map(x => x.LastUpdateTime);
        }
    }
}