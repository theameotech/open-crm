using OpenCRM.DB.DomainObjects;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenCRM.DB.Mapping
{
    public class BidsMap : ClassMap<Bids>
    {
        public BidsMap()
        {
            Table("bids");

            Id(x => x.Id).Not.Nullable();
            Map(x => x.VehicleId).Not.Nullable();
            Map(x => x.BuyerId).Not.Nullable();
            Map(x => x.Type).Not.Nullable();
            Map(x => x.CreateTime).Not.Nullable();
            Map(x => x.LastUpdateTime);
            Map(x => x.AuctionId).Not.Nullable();
            Map(x => x.Amount);
        }
    }
}