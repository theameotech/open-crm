using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenCRM.Common.DTO
{
    public class AuctionBidsBuyers
    {
        public string Model { get; set; } //Vehicle info
        public string Name { get; set; } //Vehicle info
        public long? Odometer { get; set; } //Vehicle info
        public string VIN { get; set; } //Vehicle info
        public string BidType { get; set; } //Bid info
        public string Ammount { get; set; } //Bid info
        public string BuyerName { get; set; } //Buyer Info
    }
}