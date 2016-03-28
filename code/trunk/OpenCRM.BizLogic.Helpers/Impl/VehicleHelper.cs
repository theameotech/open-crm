using OpenCRM.BizLogic.Helpers.Interfaces;
using OpenCRM.DB.DomainObjects;
using OpenCRM.DB.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.BizLogic.Helpers.Impl
{
   public class VehicleHelper : IVehicleHelper
    {
       private IVehicleRepo _vehicleRepo;
        private IBuyerRepo _buyerRepo;
        public VehicleHelper(IBuyerRepo buyerRepo, IVehicleRepo vehicleRepo)
        {
            _vehicleRepo = vehicleRepo;
            _buyerRepo = buyerRepo;
        
        }

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

        public IList<Vehicle> GetAll()
        {
            return _vehicleRepo.FetchAll();
        }
    }
}
