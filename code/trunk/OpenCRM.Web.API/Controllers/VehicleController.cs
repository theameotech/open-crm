using OpenCRM.BusinessManagers.Interfaces;
using OpenCRM.DB.DomainObjects;
using OpenCRM.DB.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OpenCRM.Web.API
{
    [RoutePrefix("api/vehicle")]
    public class VehicleController : ApiController
    {
        private IVehicleManager _vehicleManage;

        public VehicleController(IVehicleManager vehicleManage)
        {
            _vehicleManage = vehicleManage;
         
        }

        [Route("Createvehicle")]
        public void CreateVehicle(Vehicle vhcle)
        {
            _vehicleManage.CreateVehicle(vhcle);
        }

        [Route("All")]
        public IList<Vehicle> GetAll()
        {
            return _vehicleManage.GetAll();
        }

        //[Route("GetVehicleInfoAuction")]
        //public PageList<VehicleCore> GetVehicleInfoByAuction(int auctionId, int page, int pageSize)
        //{
        //    return _buyerRepo.GetVehicleInfoByAuction(auctionId, page, pageSize);
        //}
    }
}
