using CsvHelper;
using OpenCRM.BizLogic.Helpers.Interfaces;
using OpenCRM.Common;
using OpenCRM.Common.DTO;
using OpenCRM.DB.DomainObjects;
using OpenCRM.DB.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace OpenCRM.BizLogic.Helpers.Impl
{
    public class BuyerHelper : IBuyerHelper
    {

        private IBuyerRepo _buyersRepo;
        private ISearchParamsRepo _srarchParamsRepo;
        private IVehicleRepo _vehicleRepo;
        public BuyerHelper(IBuyerRepo buyersRepo, IVehicleRepo vehicleRepo, ISearchParamsRepo srarchParamsRepo)
        {
            _buyersRepo = buyersRepo;
            _vehicleRepo = vehicleRepo;
            _srarchParamsRepo = srarchParamsRepo;
        }
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

        public void UpdateBuyer(Buyers buyers)
        {
            var buyer = _buyersRepo.Get(x => x.Id == buyers.Id);
            buyer.ContactFirstName = buyers.ContactFirstName;
            buyer.ContactLastName = buyers.ContactLastName;
            buyer.BuyerAddress = buyers.BuyerAddress;
            buyer.BuyerPhone = buyers.BuyerPhone;
            buyer.BuyerEmail = buyers.BuyerEmail;
            buyer.Name = buyers.Name;
            _buyersRepo.Update(buyer);

        }

        public IList<Buyers> GetAll()
        {
            return _buyersRepo.FetchAll();
        }

        public PageList<Buyers> GetMostActive(int timeInterval, string intervalType, int page, int pageSize)
        {
            return _buyersRepo.FetchActiveBuyerWithInTimeInterval(timeInterval, intervalType, page, pageSize);
        }

        public IList<VehicleInfo> GetVehicleInfoByBuyer(int buyerId, string searchText, int modelMin, int modelMax, int odometeMin, int odomoterMax, DateTime minDate, DateTime maxDate)
        {
            return _buyersRepo.GetVehicleInfoByBuyer(buyerId, searchText, modelMin, modelMax,
                 odometeMin, odomoterMax, minDate, maxDate);
        }

        public PageList<VehicleCore> GetVehicleInfoByAuction(int auctionId, int page, int pageSize)
        {
            return _buyersRepo.GetVehicleInfoByAuction(auctionId, page, pageSize);
        }

        public PageList<VehicleInfo> GetBidsInfoByVehicle(int vehicleId, int page, int pageSize)
        {
            return _buyersRepo.GetBidsInfoByVehicle(vehicleId, page, pageSize);
        }

        public SearchParams GetSearchParams()
        {
            var sParams = _srarchParamsRepo.FetchAll();
            if (sParams.Count > 0)
                return sParams.FirstOrDefault();

            return new SearchParams();
        }

        public PageList<Buyers> Search(int page, int pageSize, string searchText, int modelMin, int modelMax, int odometeMin, int odomoterMax, DateTime maxDate, DateTime minDate)
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
        public void WriteHeaderRow(CsvWriter cw)
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

        public void ExportSearchResultToCsv(string searchText, int modelMin, int modelMax, int odometeMin, int odomoterMax, DateTime minDate, DateTime maxDate)
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
