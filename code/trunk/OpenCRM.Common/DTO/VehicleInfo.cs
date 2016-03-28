using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenCRM.Common.DTO
{
    public class VehicleInfo : VehicleCore
    {
        public string BuyerName { get; set; }

        public string BuyerAddress { get; set; }

        public string Amount { get; set; }
        public string Type { get; set; }


        public DateTime AuctionDate { get; set; }

        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }
        public string BuyerEmail { get; set; }
        public string BuyerPhone { get; set; }
       
    }

    public class VehicleCore : Core
    {

        public string Name { get; set; }
        public string Model { get; set; }
        public string Vin { get; set; }
        public long Odometer { get; set; }
    }

    public class Core
    {
        public int Id { get; set; }
    }
}