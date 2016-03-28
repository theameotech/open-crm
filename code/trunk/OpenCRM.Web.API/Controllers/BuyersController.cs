using CsvHelper;
using OpenCRM.BusinessManagers.Interfaces;
using OpenCRM.Common;
using OpenCRM.Common.DTO;
using OpenCRM.DB.DomainObjects;
using OpenCRM.DB.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Transactions;
using System.Web;
using System.Web.Http;

namespace OpenCRM.Web.API
{
    [RoutePrefix("api/buyers")]
    public class BuyersController : ApiController
    {
        private IBuyerManager _buyerManager;

        public BuyersController(IBuyerManager buyerManager)
        {
            _buyerManager = buyerManager;
            
        }

        [Route("CreateBuyer")]
        public void CreateBuyer(Buyers buyers)
        {
            _buyerManager.CreateBuyer(buyers);
        }
        [Route("UpdateBuyer")]
        public void UpdateBuyer(Buyers buyers)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    _buyerManager.UpdateBuyer(buyers);
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
            return _buyerManager.GetAll();
        }

        [Route("GetMostActive")]
        public PageList<Buyers> GetMostActive(int timeInterval, string intervalType, int page, int pageSize)
        {
            return _buyerManager.GetMostActive(timeInterval, intervalType, page, pageSize);
        }

        [Route("GetVehicleInfoBuyer")]
        public IList<VehicleInfo> GetVehicleInfoByBuyer(int buyerId, string searchText,
            int modelMin, int modelMax, int odometeMin, int odomoterMax, DateTime minDate, DateTime maxDate)
        {
           return _buyerManager.GetVehicleInfoByBuyer(buyerId,searchText,modelMin,modelMax,odometeMin,odomoterMax,minDate,maxDate);
        }

        [Route("GetVehicleInfoByAuction")]
        public PageList<VehicleCore> GetVehicleInfoByAuction(int auctionId, int page, int pageSize)
        {
            return _buyerManager.GetVehicleInfoByAuction(auctionId, page, pageSize);
        }

        [Route("GetBidsInfoByVehicle")]
        public PageList<VehicleInfo> GetBidsInfoByVehicle(int vehicleId, int page, int pageSize)
        {
            return _buyerManager.GetBidsInfoByVehicle(vehicleId, page, pageSize);
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
            return _buyerManager.GetSearchParams(); 
        }

        [Route("Search")]
        [HttpGet]
        public PageList<Buyers> Search(int page, int pageSize, string searchText,
            int modelMin, int modelMax, int odometeMin, int odomoterMax, DateTime maxDate, DateTime minDate)
        {
            return _buyerManager.Search(page, pageSize, searchText, modelMin, modelMax, odometeMin, odomoterMax, maxDate, minDate);
        }

        //private string HeaderRow
        //{
        //    get
        //    {
        //        return string.Format("Dealership,Address,Phone,Year,Make Model,Vehicle Odometer,Vehicle VIN,Amount,Bid Type,Auction Date");
        //    }
        //}

        private void WriteHeaderRow(CsvWriter cw)
        {
            _buyerManager.WriteHeaderRow(cw);
        }

        //public static byte[] ReadToEnd(StreamReader reader)
        //{
        //    var bytes = default(byte[]);
        //    using (var memstream = new MemoryStream())
        //    {
        //        var buffer = new byte[512];
        //        var bytesRead = default(int);
        //        while ((bytesRead = reader.BaseStream.Read(buffer, 0, buffer.Length)) > 0)
        //            memstream.Write(buffer, 0, bytesRead);
        //        bytes = memstream.ToArray();
        //    }
        //    return bytes;
        //}

        [Route("Export")]
        [HttpGet]
        [AllowAnonymous]
        public void ExportSearchResultToCsv(string searchText,
            int modelMin, int modelMax, int odometeMin, int odomoterMax, DateTime minDate, DateTime maxDate)
        {
            _buyerManager.ExportSearchResultToCsv(searchText, modelMin, modelMax, odometeMin, odomoterMax, minDate, maxDate);
        }
    }
}
