using AC.Dto;
using AC.Filters;
using AC.Models;
using AC.Repository;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Transactions;
using System.Web;
using System.Web.Http;

namespace OpenCRM.Web.Controllers
{
    [RoutePrefix("api/buyers")]
    [AuthorizedUser]
    public class BuyersController : ApiController
    {
        private IBuyerRepo _buyersRepo;
        private ISearchParamsRepo _srarchParamsRepo;
        private IRepository<Vehicle> _vehicleRepo;
        public BuyersController(IBuyerRepo buyersRepo, IRepository<Vehicle> vehicleRepo, ISearchParamsRepo srarchParamsRepo)
        {
            _buyersRepo = buyersRepo;
            _vehicleRepo = vehicleRepo;
            _srarchParamsRepo = srarchParamsRepo;
        }

        [Route("CreateBuyer")]
        public void CreateBuyer(Buyers buyers)
        {
            Buyers buyer = new Buyers();
            buyer.Name = buyers.Name;
            buyer.ContactFirstName = buyers.ContactFirstName;
            buyer.ContactLastName = buyers.ContactLastName;
            buyer.BuyerEmail = buyers.BuyerEmail;
            buyer.BuyerAddress = buyers.BuyerAddress;
            buyer.BuyerPhone = buyers.BuyerPhone;
            buyer.Name = buyers.Name;

            _buyersRepo.Add(buyer);
        }
        [Route("UpdateBuyer")]
        public void UpdateBuyer(Buyers buyers)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    var buyer = _buyersRepo.Get(x => x.Id == buyers.Id);
                    buyer.ContactFirstName = buyers.ContactFirstName;
                    buyer.ContactLastName = buyers.ContactLastName;
                    buyer.BuyerAddress = buyers.BuyerAddress;
                    buyer.BuyerPhone = buyers.BuyerPhone;
                    buyer.BuyerEmail = buyers.BuyerEmail;
                    buyer.Name = buyers.Name;
                    _buyersRepo.Update(buyer);
                    scope.Complete();
                }
                catch (Exception)
                {
                    scope.Dispose();
                }
            }
        }

        [Route("All")]
        public IList<Buyers> GetAll()
        {
            return _buyersRepo.FetchAll();
        }

        [Route("GetMostActive")]
        public PageList<Buyers> GetMostActive(int timeInterval, string intervalType, int page, int pageSize)
        {
            return _buyersRepo.FetchActiveBuyerWithInTimeInterval(timeInterval, intervalType, page, pageSize);
        }

        [Route("GetVehicleInfoBuyer")]
        public IList<VehicleInfo> GetVehicleInfoByBuyer(int buyerId, string searchText,
            int modelMin, int modelMax, int odometeMin, int odomoterMax, DateTime minDate, DateTime maxDate)
        {
            return _buyersRepo.GetVehicleInfoByBuyer(buyerId, searchText, modelMin, modelMax,
                odometeMin, odomoterMax, minDate, maxDate);
        }

        [Route("GetVehicleInfoByAuction")]
        public PageList<VehicleCore> GetVehicleInfoByAuction(int auctionId, int page, int pageSize)
        {
            return _buyersRepo.GetVehicleInfoByAuction(auctionId, page, pageSize);
        }

        [Route("GetBidsInfoByVehicle")]
        public PageList<VehicleInfo> GetBidsInfoByVehicle(int vehicleId, int page, int pageSize)
        {
            return _buyersRepo.GetBidsInfoByVehicle(vehicleId, page, pageSize);
        }

        //[Route("GetSearchParams")]
        //public SearchParams GetSearchParams()
        //{
        //   // var vehicles = _vehicleRepo.FetchAll();
        //    //bool sts = vehicles.Select(x => x.OdoMeter).Any();

        //    //var minOdoMeter = 1000;
        //    //var maxOdoMeter = 99999;

        //    //if (sts != false)
        //    //{
        //    //     minOdoMeter = vehicles.Select(x => Convert.ToInt32(x.OdoMeter)).Min();
        //    //     maxOdoMeter = vehicles.Select(x => Convert.ToInt32(x.OdoMeter)).Max();
        //    //}
        //    var queryResult = _buyersRepo.GetMinMaxValues();
        //    //var minOdoMeter = vehicles.Select(x => Convert.ToInt32(x.OdoMeter)).Min();
        //    //var maxOdoMeter = vehicles.Select(x => Convert.ToInt32(x.OdoMeter)).Max();
        //    //var minModel = vehicles.Select(x => Convert.ToInt32(x.Model)).Min();
        //    //var maxModel = vehicles.Select(x => Convert.ToInt32(x.Model)).Max();
        //    //var maxDate = vehicles.Select(x => x.CreateTime).Max();
        //    //var minDate = vehicles.Select(x => x.CreateTime).Min();
        //    var searchParams = new SearchParams();
        //    if (queryResult != null && queryResult.Count > 0)
        //    {
        //        searchParams.OdoMeterMinValue = Convert.ToInt32(queryResult[0].MinOdometer);
        //        searchParams.OdoMeterMaxValue =  Convert.ToInt32(queryResult[0].MaxOdometer);
        //        searchParams.ModelMinValue = !string.IsNullOrEmpty(queryResult[0].MinModel) ? Convert.ToInt32(queryResult[0].MinModel) : 0;
        //        searchParams.ModelMaxValue = Convert.ToInt32(queryResult[0].MaxModel);
        //        searchParams.MinDate = Convert.ToDateTime(queryResult[0].MinCreateDate);
        //        searchParams.MaxDate = Convert.ToDateTime(queryResult[0].MaxCreateDate);

        //    }
        //    return searchParams;
        //}

        [Route("GetSearchParams")]
        public SearchParams GetSearchParams()
        {
            var sParams = _srarchParamsRepo.FetchAll();
            if (sParams.Count > 0)
                return sParams.FirstOrDefault();

            return new SearchParams();
        }

        [Route("Search")]
        [HttpGet]
        public PageList<Buyers> Search(int page, int pageSize, string searchText,
            int modelMin, int modelMax, int odometeMin, int odomoterMax, DateTime maxDate, DateTime minDate)
        {
            return _buyersRepo.Search(page, pageSize, searchText, modelMin,
                modelMax, odometeMin, odomoterMax, maxDate, minDate);
        }

        private string HeaderRow
        {
            get
            {
                return string.Format("Dealership,Address,Phone,Year,Make Model,Vehicle Odometer,Vehicle VIN,Amount,Bid Type,Auction Date");
            }
        }

        private void WriteHeaderRow(CsvWriter cw)
        {
            cw.WriteField("Dealership");
            cw.WriteField("Address");
            cw.WriteField("Phone");
            cw.WriteField("Year");
            cw.WriteField("Make Model");
            cw.WriteField("Vehicle Odometer");
            cw.WriteField("Vehicle VIN");
            cw.WriteField("Amount");
            cw.WriteField("Bid Type");
            cw.WriteField("Auction Date");
            cw.NextRecord();
        }

        public static byte[] ReadToEnd(StreamReader reader)
        {
            var bytes = default(byte[]);
            using (var memstream = new MemoryStream())
            {
                var buffer = new byte[512];
                var bytesRead = default(int);
                while ((bytesRead = reader.BaseStream.Read(buffer, 0, buffer.Length)) > 0)
                    memstream.Write(buffer, 0, bytesRead);
                bytes = memstream.ToArray();
            }
            return bytes;
        }

        [Route("Export")]
        [HttpGet]
        [AllowAnonymous]
        public void ExportSearchResultToCsv(string searchText,
            int modelMin, int modelMax, int odometeMin, int odomoterMax, DateTime minDate, DateTime maxDate)
        {
            string random = new Random().Next(100000).ToString();

            string csvPath = string.Format("{0}", System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/temp"),
                string.Format("Buyers_{0}.csv", random)));

            using (var sw = new StreamWriter(csvPath))
            {
                var writer = new CsvHelper.CsvWriter(sw);
                var buyers = _buyersRepo.GetBuyerRecords(searchText, modelMin,
                modelMax, odometeMin, odomoterMax, maxDate, minDate);

                WriteHeaderRow(writer);

                foreach (var buyer in buyers)
                {
                    writer.WriteField(buyer.Name);
                    writer.WriteField(buyer.BuyerAddress);
                    writer.WriteField(buyer.BuyerPhone);
                    writer.NextRecord();

                    var buyerVehicles = _buyersRepo.GetVehicleInfoByBuyer(buyer.Id, searchText,
                        modelMin, modelMax, odometeMin, odomoterMax, minDate, maxDate);

                    foreach (var vehicle in buyerVehicles)
                    {
                        writer.WriteField("");
                        writer.WriteField("");
                        writer.WriteField("");
                        writer.WriteField(vehicle.Model);
                        writer.WriteField(vehicle.Name);
                        writer.WriteField(vehicle.Odometer);
                        writer.WriteField(vehicle.Vin);
                        writer.WriteField(vehicle.Amount);
                        writer.WriteField(vehicle.Type);
                        writer.WriteField(vehicle.AuctionDate);
                        writer.NextRecord();
                    }
                }
            }

            byte[] bytes = new byte[0];
            using (StreamReader sr = new StreamReader(csvPath))
            {
                bytes = ReadToEnd(sr);
            }
            HttpResponse response = HttpContext.Current.Response;
            response.ContentType = "text/csv";
            response.AppendHeader("Content-Disposition", "attachment;filename=" + string.Format("Buyers_{0}.csv", random));
            response.OutputStream.Write(bytes, 0, bytes.Length - 1);
            response.End();
        }
    }
}
