using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CsvHelper.TypeConversion;
using CsvHelper.Configuration;
namespace OpenCRM.Common.DTO
{
    public class AuctionBidWithBuyers
    {
        public AuctionBidWithBuyers()
        {
            OnlineBidder = new Bidder();
            SoldBidder = new Bidder();
            IfSaleBidder = new Bidder();
        }

        public string Model { get; set; } //Vehicle info
        public string Car { get; set; } //Vehicle info
        public string Odometer { get; set; } //Vehicle info
        public string VIN { get; set; } //Vehicle info
        public string BidType { get; set; } //Bid info
        public string Ammount { get; set; } //Bid info

        public Bidder OnlineBidder { get; set; }
        public Bidder SoldBidder { get; set; }
        public Bidder IfSaleBidder { get; set; }
    }

    public class Bidder
    {
        public string Name { get; set; }
        public string Ammount { get; set; } //Bid info
        public string Address { get; set; }
        public string Phone { get; set; }

        public bool IsEmpty()
        {
            string data = Name + Ammount + Address + Phone;
            if (!string.IsNullOrEmpty(data))
                return false;

            return true;
        }
    }

    public sealed class OnlineBidderMap : CsvClassMap<Bidder>
    {
        public OnlineBidderMap()
        {
            Map(m => m.Name).Name("Online bid");
            Map(m => m.Ammount).Name("OnlineBidDollars");
            Map(m => m.Address).Name("OnlineBidAddress");
            Map(m => m.Phone).Name("OnlineBidPhone");
        }
    }
    public sealed class SoldBidderMap : CsvClassMap<Bidder>
    {
        public SoldBidderMap()
        {
           
            Map(m => m.Name).Name("Sold");
            Map(m => m.Ammount).Name("SoldDollars");
            Map(m => m.Address).Name("SoldAddress");
            Map(m => m.Phone).Name("SoldPhone No.");
        }
    }
    public sealed class IfSaleBidderMap : CsvClassMap<Bidder>
    {
        public IfSaleBidderMap()
        {
            Map(m => m.Name).Name("If sale");
            Map(m => m.Ammount).Name("IfsaleDollars");
            Map(m => m.Address).Name("IfsaleAddress");
            Map(m => m.Phone).Name("IfsalePhone No.");
        }
    }



    public class CsvMapping : CsvHelper.Configuration.CsvClassMap<AuctionBidWithBuyers>
    {
        public CsvMapping()
        {
            Map(m => m.Model).Name("Model");
            Map(m => m.Car).Name("Car");
            Map(m => m.Odometer).Name("Odometer");
            Map(m => m.VIN).Name("VIN number");            

            References<OnlineBidderMap>(m => m.OnlineBidder);
            References<SoldBidderMap>(m => m.SoldBidder);
            References<IfSaleBidderMap>(m => m.IfSaleBidder);                      
        }
    }
}