using OpenCRM.DB.DomainObjects;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenCRM.DB.Mapping
{
    public class CountryMap : ClassMap<Country>
    {
        public CountryMap()
        {
            Table("country");

            Id(x => x.Id).Not.Nullable();
            Map(x => x.ISO).Not.Nullable();
            Map(x => x.Name).Not.Nullable();
            Map(x => x.NiceName);
            Map(x => x.ISO3);
            Map(x => x.NumCode);
            Map(x => x.PhoneCode);
        }
    }
}