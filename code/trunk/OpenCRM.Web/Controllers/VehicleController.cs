using AC.Dto;
using AC.Models;
using AC.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OpenCRM.Web.Controllers
{
    [RoutePrefix("api/vehicle")]
    public class VehicleController : ApiController
    {
        private IRepository<Vehicle> _vehicleRepo;
        private IBuyerRepo _buyerRepo;
        public VehicleController(IRepository<Vehicle> vehicleRepo, IBuyerRepo buyerRepo)
        {
            _vehicleRepo = vehicleRepo;
            _buyerRepo = buyerRepo;
        }

        [Route("Createvehicle")]
        public void CreateVehicle(Vehicle vhcle)
        {
            Vehicle vehicle = new Vehicle();
            vehicle.Name = vehicle.Name;
            vehicle.AuctionId = vhcle.AuctionId;
            vehicle.Name = vhcle.Name;
            vehicle.Model = vhcle.Model;
            vehicle.OdoMeter = vhcle.OdoMeter;
            vehicle.Vin = vhcle.Vin;
            _vehicleRepo.Add(vehicle);
        }

        [Route("All")]
        public IList<Vehicle> GetAll()
        {
            return _vehicleRepo.FetchAll();
        }

        //[Route("GetVehicleInfoAuction")]
        //public PageList<VehicleCore> GetVehicleInfoByAuction(int auctionId, int page, int pageSize)
        //{
        //    return _buyerRepo.GetVehicleInfoByAuction(auctionId, page, pageSize);
        //}
    }
}
