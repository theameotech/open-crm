using AC.Dto;
using AC.Filters;
using AC.Models;
using AC.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;


namespace OpenCRM.Web.Controllers
{
    [RoutePrefix("api/auction")]
    [AuthorizedUser]
    public class AuctionController : ApiController
    {
        private string basPath = HttpContext.Current.Server.MapPath("~/temp");
        private IAuctionRepo _auctionRepo;
        private IBuyerRepo _buyerRepo;
        public AuctionController(IAuctionRepo auctionRepo, IBuyerRepo buyerRepo)
        {
            _auctionRepo = auctionRepo;
            _buyerRepo = buyerRepo;

        }

        [Route("CreateAuction")]
        public void CreateAuction(Auction auct)
        {
            try
            {
                Auction auction = new Auction();
                auction.Name = auct.Name;
                auction.Address = auct.Address;
                auction.Store = auct.Store;
                _auctionRepo.Add(auction);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("All")]
        public IList<Auction> GetAll()
        {
            var auctions = _auctionRepo.FetchAll();
            return auctions;
        }

        [Route("PostBids")]
        public HttpResult Post(AuctionBids auction)
        {
            try
            {
                //First validate aucrion object.
                auction.Validate();

                string random = new Random().Next(100000).ToString();
                string filePath = System.IO.Path.Combine(basPath, string.Format("{0}.csv",
                    random));

                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.Write(auction.File);
                    writer.Flush();
                }

                CsvHelper.CsvReader reader = new CsvHelper.CsvReader(new StreamReader(filePath));


                if (auction.ImportMode == "OLD")
                {
                    reader.Configuration.RegisterClassMap<CsvMapping>();
                    var list = reader.GetRecords<AuctionBidWithBuyers>().ToList();

                    _auctionRepo.CreateAuctionAndBids(auction.AuctionName, Convert.ToDateTime(auction.AuctionDate), auction.Id, list);
                }
                else
                {

                    var list = reader.GetRecords<AuctionBidsBuyers>().ToList();
                    _auctionRepo.ImportAuctionAndBids(auction.AuctionName, Convert.ToDateTime(auction.AuctionDate), auction.Id, list);
                }

                return new HttpResult
                {
                    Message = "Auction has been submitted.",
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new HttpResult
                {
                    Message = ex.Message,
                    Success = false
                };
            }
        }
    }

}
