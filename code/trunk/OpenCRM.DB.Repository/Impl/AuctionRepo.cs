using NHibernate;
using OpenCRM.Common.DTO;
using OpenCRM.DB.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace OpenCRM.DB.Repository
{
    public class AuctionRepo : Repository<Auction>, IAuctionRepo
    {
        private IRepository<Vehicle> _vehicleRepo;
        private IRepository<Bids> _bidRepo;
        private IBuyerRepo _buyerRepo;
        private ISearchParamsRepo _srarchParamsRepo;
        public AuctionRepo(ISession session, IRepository<Vehicle> vehicleRepo,
            IBuyerRepo buyerRepo, IRepository<Bids> bidRepo, ISearchParamsRepo srarchParamsRepo)
            : base(session)
        {
            _vehicleRepo = vehicleRepo;
            _buyerRepo = buyerRepo;
            _bidRepo = bidRepo;
            _srarchParamsRepo = srarchParamsRepo;
        }


        /// <summary>
        /// Executed when we are importing new format of the file.
        /// </summary>
        /// <param name="auction"></param>
        /// <param name="auctionBids"></param>
        public void ImportAuctionAndBids(string auctioName, DateTime auctionDate, int id, IList<AuctionBidsBuyers> auctionBids)
        {
            Auction ac = null;// Get(x => x.Name == auctioName);
            if (id > 0)
            {
                ac = Get(x => x.Id == id);
                if (ac == null)
                {
                    throw new Exception(string.Format("auction with#{0} does not exist", id));
                }
            }
            else
            {
                ac = new Auction();

                ac.AuctionDate = Convert.ToDateTime(auctionDate);
                ac.Name = auctioName;

                SaveAuction(ac);
            }

            for (var index = 0; index <= auctionBids.Count - 1; index++)
            {
                var auctionbid = auctionBids[index];

                var vehicle = _vehicleRepo.Get(x => x.Vin == auctionbid.VIN /*&&
                    x.Model == auctionbid.Model && x.Name == auctionbid.Name
                    && x.OdoMeter == Convert.ToInt64(auctionbid.Odometer)*/);

                if (vehicle == null)
                {
                    vehicle = new Vehicle();
                    //let's save vehicle here
                    vehicle.Name = auctionbid.Name;
                    vehicle.Vin = auctionbid.VIN;
                    vehicle.AuctionId = ac.Id;
                    vehicle.Model = auctionBids[index].Model;
                    vehicle.OdoMeter = Convert.ToInt64(auctionBids[index].Odometer);
                    _vehicleRepo.Add(vehicle);
                }

                //Read the bids against this vehicle and store it.
                var bids = GetBidsForAnAuction((index + 1), auctionBids);
                foreach (var bid in bids)
                {
                    //Let's add buyer in DB
                    Buyers buyer = _buyerRepo.Get(x => x.Name == bid.BuyerName);

                    if (buyer == null)
                    {
                        buyer = new Buyers();
                        buyer.Name = bid.BuyerName;
                        _buyerRepo.Add(buyer);
                    }

                    //Finally add bid.
                    var buyerBid = new Bids();
                    buyerBid.VehicleId = vehicle.Id;
                    buyerBid.BuyerId = buyer.Id;
                    buyerBid.Type = bid.BidType;
                    buyerBid.AuctionId = ac.Id;

                    _bidRepo.Add(buyerBid);
                }
                index += bids.Count;
            }
            SaveSearchparams();
        }

        private void SaveAuction(Auction auction)
        {
            if (auction.Id <= 0)
            {
                this.Add(auction);
            }
        }

        public void CreateAuctionAndBids(string auctioName, DateTime auctionDate, int auctionId, IList<AuctionBidWithBuyers> auctionBids)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TimeSpan(0, 15, 0)))
            {
                Auction ac = null;// Get(x => x.Name == auctioName);
                if (auctionId > 0)
                {
                    ac = Get(x => x.Id == auctionId);
                    if (ac == null)
                    {
                        throw new Exception(string.Format("auction with#{0} does not exist", auctionId));
                    }
                }
                else
                {
                    ac = new Auction()
                    {
                        AuctionDate = Convert.ToDateTime(auctionDate),
                        Name = auctioName
                    };
                    SaveAuction(ac);
                }
                //var ac = Get(x => x.Name == auctioName);
                //if (ac == null)
                //{
                //    ac = new Auction()
                //    {
                //        AuctionDate = Convert.ToDateTime(auctionDate),
                //        Name = auctioName
                //    };
                //    SaveAuction(ac);
                //}

                for (var index = 0; index <= auctionBids.Count - 1; index++)
                {
                    var auctionbid = auctionBids[index];

                    var vehicle = _vehicleRepo.Get(x => x.Vin == auctionbid.VIN /*&&
                        x.Model == auctionbid.Model && x.Name == auctionbid.Car
                        && x.OdoMeter == auctionbid.Odometer*/);

                    if (vehicle == null)
                    {
                        //let's save vehicle here
                        vehicle = new Vehicle();
                        vehicle.Name = auctionbid.Car;
                        vehicle.Vin = auctionbid.VIN;
                        vehicle.AuctionId = ac.Id;
                        vehicle.Model = auctionbid.Model;
                        vehicle.OdoMeter = Convert.ToInt32(auctionbid.Odometer);
                        _vehicleRepo.Add(vehicle);
                    }

                    SaveBuyerWithBids(ac, vehicle, auctionbid);

                    //Read the bids against this vehicle and store it.
                    var bids = GetBidsForAnAuction((index + 1), auctionBids);
                    foreach (var bid in bids)
                    {
                        SaveBuyerWithBids(ac, vehicle, bid);
                    }
                    index += bids.Count;
                }
                SaveSearchparams();
                scope.Complete();
            }
        }

        private void SaveBuyerWithBids(Auction auction, Vehicle vehicle, AuctionBidWithBuyers bid)
        {
            Buyers buyer = null;

            if (bid.OnlineBidder != null && !bid.OnlineBidder.IsEmpty())
            {
                buyer = SaveBidder(bid.OnlineBidder);
                bid.BidType = "ONLINE";
                AddBid(auction.Id, vehicle.Id, bid.OnlineBidder.Ammount, buyer.Id, bid.BidType);
            }

            if (bid.SoldBidder != null && !bid.SoldBidder.IsEmpty())
            {
                buyer = SaveBidder(bid.SoldBidder);
                bid.BidType = "SOLD";
                AddBid(auction.Id, vehicle.Id, bid.SoldBidder.Ammount, buyer.Id, bid.BidType);
            }

            if (bid.IfSaleBidder != null && !bid.IfSaleBidder.IsEmpty())
            {
                buyer = SaveBidder(bid.IfSaleBidder);
                bid.BidType = "IFSALE";
                AddBid(auction.Id, vehicle.Id, bid.IfSaleBidder.Ammount, buyer.Id, bid.BidType);
            }

        }

        private void AddBid(int auctionId, int vehicleId, string ammount, int buyerId, string type)
        {
            var buyerBid = _bidRepo.Get(x => x.VehicleId == vehicleId
                && x.BuyerId == buyerId && x.AuctionId == auctionId);

            if (buyerBid == null)
            {
                buyerBid = new Bids()
                {
                    VehicleId = vehicleId,
                    BuyerId = buyerId,
                    Type = type,
                    AuctionId = auctionId,
                    Amount = ammount
                };
            }
            else
            {
                buyerBid.Amount = ammount;
                buyerBid.Type = type;
            }

            _bidRepo.Add(buyerBid);
        }

        public Buyers SaveBidder(Bidder bidder)
        {
            Buyers buyer = _buyerRepo.Get(x => x.Name == bidder.Name);

            if (buyer == null)
            {
                buyer = new Buyers();
                buyer.Name = bidder.Name;
                buyer.BuyerAddress = bidder.Address;
                buyer.BuyerPhone = bidder.Phone;
                buyer.Amount = bidder.Ammount;
                _buyerRepo.Add(buyer);
            }
            return buyer;
        }

        private IList<AuctionBidWithBuyers> GetBidsForAnAuction(int currentIndex, IList<AuctionBidWithBuyers> auctionBids)
        {
            List<AuctionBidWithBuyers> bids = new List<AuctionBidWithBuyers>();
            for (var index = currentIndex; index <= auctionBids.Count - 1; index++)
            {
                var auction = auctionBids[index];
                if (!string.IsNullOrEmpty(auction.VIN))
                {
                    break;
                }
                bids.Add(auction);
            }
            return bids;
        }

        private IList<AuctionBidsBuyers> GetBidsForAnAuction(int currentIndex, IList<AuctionBidsBuyers> auctionBids)
        {
            List<AuctionBidsBuyers> bids = new List<AuctionBidsBuyers>();
            for (var index = currentIndex; index <= auctionBids.Count - 1; index++)
            {
                var auction = auctionBids[index];
                if (!string.IsNullOrEmpty(auction.VIN))
                {
                    break;
                }
                bids.Add(auction);
            }
            return bids;
        }

        public void SaveSearchparams()
        {
            SearchParams searchparam = new SearchParams();

            var searchResult = _srarchParamsRepo.FetchAll();
            if (searchResult.Count >= 0)
            {
                searchparam = searchResult.FirstOrDefault();
            }
            var queryResult = _buyerRepo.GetMinMaxValues();            
            if (queryResult != null && queryResult.Count > 0)
            {
                searchparam.OdoMeterMinValue = Convert.ToInt32(queryResult[0].MinOdometer);
                searchparam.OdoMeterMaxValue = Convert.ToInt32(queryResult[0].MaxOdometer);
                searchparam.ModelMinValue = !string.IsNullOrEmpty(queryResult[0].MinModel) ? Convert.ToInt32(queryResult[0].MinModel) : 0;
                searchparam.ModelMaxValue = Convert.ToInt32(queryResult[0].MaxModel);
                searchparam.MinDate = Convert.ToDateTime(queryResult[0].MinCreateDate);
                searchparam.MaxDate = Convert.ToDateTime(queryResult[0].MaxCreateDate);

            }
            _srarchParamsRepo.Add(searchparam);
        }
    }
}
