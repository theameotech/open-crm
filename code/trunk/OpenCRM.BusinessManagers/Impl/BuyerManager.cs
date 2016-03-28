using OpenCRM.BusinessManagers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCRM.DB.DomainObjects;
using OpenCRM.Common;
using OpenCRM.Common.DTO;
using CsvHelper;
using OpenCRM.BizLogic.Helpers.Interfaces;
using System.IO;

namespace OpenCRM.BusinessManagers.Impl
{
   public class BuyerManager:IBuyerManager
    {
       private IBuyerHelper _buyerHelper;
       public BuyerManager(IBuyerHelper buyerHelper) 
       {
           _buyerHelper = buyerHelper;
       }

        public void CreateBuyer(Buyers buyers)
        {
            _buyerHelper.CreateBuyer(buyers);
        }

        public void UpdateBuyer(Buyers buyers)
        {
            _buyerHelper.UpdateBuyer(buyers);
        }

        public IList<Buyers> GetAll()
        {
            return _buyerHelper.GetAll();
        }

        public PageList<Buyers> GetMostActive(int timeInterval, string intervalType, int page, int pageSize)
        {
            return _buyerHelper.GetMostActive(timeInterval, intervalType, page, pageSize);
        }

        public IList<VehicleInfo> GetVehicleInfoByBuyer(int buyerId, string searchText, int modelMin, int modelMax, int odometeMin, int odomoterMax, DateTime minDate, DateTime maxDate)
        {
            return _buyerHelper.GetVehicleInfoByBuyer(buyerId, searchText, modelMin, modelMax, odometeMin, odomoterMax, minDate, maxDate);
        }

        public PageList<VehicleCore> GetVehicleInfoByAuction(int auctionId, int page, int pageSize)
        {
            return _buyerHelper.GetVehicleInfoByAuction(auctionId, page, pageSize);
        }

        public PageList<VehicleInfo> GetBidsInfoByVehicle(int vehicleId, int page, int pageSize)
        {
            return _buyerHelper.GetBidsInfoByVehicle(vehicleId, page, pageSize);
        }

        public SearchParams GetSearchParams()
        {
            return _buyerHelper.GetSearchParams();
        }

        public PageList<Buyers> Search(int page, int pageSize, string searchText, int modelMin, int modelMax, int odometeMin, int odomoterMax, DateTime maxDate, DateTime minDate)
        {
            return _buyerHelper.Search(page, pageSize, searchText, modelMin, modelMax, odometeMin, odomoterMax, maxDate, minDate);
        }

        public void WriteHeaderRow(CsvWriter cw)
        {
            _buyerHelper.WriteHeaderRow(cw);
        }
        

        public void ExportSearchResultToCsv(string searchText, int modelMin, int modelMax, int odometeMin, int odomoterMax, DateTime minDate, DateTime maxDate)
        {
            _buyerHelper.ExportSearchResultToCsv(searchText, modelMin, modelMax, odometeMin, odomoterMax, minDate, maxDate);
        }
    }
}
