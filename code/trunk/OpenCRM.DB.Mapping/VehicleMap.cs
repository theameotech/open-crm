using FluentNHibernate.Mapping;
using OpenCRM.DB.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenCRM.DB.Mapping
{
    public class VehicleMap : ClassMap<Vehicle>
    {
        public VehicleMap()
        {
            Table("vehicle");

            Id(x => x.Id).Not.Nullable();
            Map(x => x.AuctionId).Not.Nullable();
            Map(x => x.Name).Not.Nullable();
            Map(x => x.Model).Not.Nullable();
            Map(x => x.OdoMeter).Not.Nullable();
            Map(x => x.Vin).Not.Nullable();
            Map(x => x.CreateTime).Not.Nullable();
            Map(x => x.LastUpdateTime);

        }
    }
}