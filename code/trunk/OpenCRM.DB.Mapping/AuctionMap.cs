using FluentNHibernate.Mapping;
using OpenCRM.DB.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.DB.Mapping
{
    public class AuctionMap : ClassMap<Auction>
    {
        public AuctionMap()
        {
            Table("auction");

            Id(x => x.Id).Not.Nullable();
            Map(x => x.Name).Not.Nullable();
            Map(x => x.Address);
            Map(x => x.AuctionDate).Not.Nullable();
            Map(x => x.CreateTime).Not.Nullable();
            Map(x => x.LastUpdateTime);
            Map(x => x.Store);
        }
    }
}
