using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenCRM.Common.DTO
{
    public class AuctionBids
    {
        public string AuctionName { get; set; }
        public string File { get; set; }
        public string AuctionDate { get; set; }
        public string ImportMode { get; set; }
        public int Id { get; set; }
        public void Validate()
        {
            if (string.IsNullOrEmpty(this.AuctionName))
                throw new Exception("Auction name is required.");

            if (string.IsNullOrEmpty(this.AuctionDate))
                throw new Exception("Auction date is required.");

            if (string.IsNullOrEmpty(this.File))
                throw new Exception("No file has been uploaded.");

            if (string.IsNullOrEmpty(this.ImportMode))
                throw new Exception("Please specify import mode for auctions.");
        }
    }
}