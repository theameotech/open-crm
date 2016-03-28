using OpenCRM.DB.DomainObjects;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenCRM.DB.Mapping
{
    public class BuyersMap : ClassMap<Buyers>
    {
        public BuyersMap()
        {
            Table("buyers");

            Id(x => x.Id).Not.Nullable();
            Map(x => x.Name).Not.Nullable();
            Map(x => x.BuyerAddress);
            Map(x => x.BuyerEmail);
            Map(x => x.BuyerPhone);
            Map(x => x.ContactFirstName);
            Map(x => x.ContactLastName);
            Map(x => x.CreateTime).Not.Nullable();
            Map(x => x.LastUpdateTime);
            Map(x => x.Amount);
        }
    }
}