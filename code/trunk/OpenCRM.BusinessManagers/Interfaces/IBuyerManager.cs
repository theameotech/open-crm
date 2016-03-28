using CsvHelper;
using OpenCRM.Common;
using OpenCRM.Common.DTO;
using OpenCRM.DB.DomainObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OpenCRM.BusinessManagers.Interfaces
{
   public interface IBuyerManager
    {
        void CreateBuyer(Buyers buyers);

        void UpdateBuyer(Buyers buyers);

        IList<Buyers> GetAll();

        PageList<Buyers> GetMostActive(int timeInterval, string intervalType, int page, int pageSize);

        IList<VehicleInfo> GetVehicleInfoByBuyer(int buyerId, string searchText,
              int modelMin, int modelMax, int odometeMin, int odomoterMax, DateTime minDate, DateTime maxDate);

        PageList<VehicleCore> GetVehicleInfoByAuction(int auctionId, int page, int pageSize);

        PageList<VehicleInfo> GetBidsInfoByVehicle(int vehicleId, int page, int pageSize);

        SearchParams GetSearchParams();

        PageList<Buyers> Search(int page, int pageSize, string searchText,
              int modelMin, int modelMax, int odometeMin, int odomoterMax, DateTime maxDate, DateTime minDate);
        void WriteHeaderRow(CsvWriter cw);

        void ExportSearchResultToCsv(string searchText,
              int modelMin, int modelMax, int odometeMin, int odomoterMax, DateTime minDate, DateTime maxDate);
    }
}
