using OpenCRM.Common;
using OpenCRM.Common.DTO;
using OpenCRM.DB.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenCRM.DB.Repository
{
    public interface IBuyerRepo : IRepository<Buyers>
    {
        /// <summary>
        /// Get most active buyers by matching them in bids
        /// </summary>
        /// <param name="timeInterval">How many interval</param>
        /// <param name="intervalType">WEEKS/MONTH/DAY/YEAR</param>
        /// <returns></returns>
        PageList<Buyers> FetchActiveBuyerWithInTimeInterval(int timeInterval, string intervalType,
            int page, int pageSize);

        IList<VehicleInfo> GetVehicleInfoByBuyer(int buyerId, string searchText,
            int modelMin, int modelMax, int odometeMin, int odomoterMax, DateTime minDate, DateTime maxDate);

        PageList<VehicleCore> GetVehicleInfoByAuction(int auctionId, int page, int pageSize);

        PageList<VehicleInfo> GetBidsInfoByVehicle(int vehicleId, int page, int pageSize);

        PageList<Buyers> Search(int page, int pageSize, string searchText,
            int modelMin, int modelMax, int odometeMin, int odomoterMax, DateTime maxDate, DateTime minDate);

        IList<Buyers> GetBuyerRecords(string searchText,
              int modelMin, int modelMax, int odometeMin, int odomoterMax, DateTime maxDate, DateTime minDate);

        IList<MinMaxValues> GetMinMaxValues();
    }
}