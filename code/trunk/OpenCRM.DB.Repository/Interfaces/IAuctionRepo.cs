using OpenCRM.Common.DTO;
using OpenCRM.DB.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.DB.Repository
{
    public interface IAuctionRepo : IRepository<Auction>
    {
        /// <summary>
        /// Used when importing old format of file with auctions and bids
        /// </summary>
        /// <param name="auction"></param>
        /// <param name="auctionBids"></param>
        void CreateAuctionAndBids(string auctioName, DateTime auctionDate,int auctionId ,IList<AuctionBidWithBuyers> auctionBids);

        /// <summary>
        /// Used when importing new format of file with auctions and bids
        /// </summary>
        /// <param name="auction"></param>
        /// <param name="auctionBids"></param>
        void ImportAuctionAndBids(string auctioName, DateTime auctionDate, int id, IList<AuctionBidsBuyers> auctionBids);
    }
}
