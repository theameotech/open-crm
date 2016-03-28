using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenCRM.DomainObjects
{
    public class AuctionContext : DbContext
    {
        public AuctionContext()
            : base("AuctionConnection")
        {

        }

        public DbSet<Auction> Auctions { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Buyers> Buyers { get; set; }

        public DbSet<Bids> Bids { get; set; }

    }
}