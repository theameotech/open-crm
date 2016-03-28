using OpenCRM.BizLogic.Helpers.Interfaces;
using OpenCRM.BusinessManagers.Interfaces;
using OpenCRM.DB.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.BusinessManagers.Impl
{
   public class VehicleManager : IVehicleManager
    {
       private IVehicleHelper _vehicleHelper;

       public VehicleManager(IVehicleHelper vehicleHelper)

       {
           _vehicleHelper = vehicleHelper;
       
       }

        public void CreateVehicle(Vehicle vhcle)
        {
            _vehicleHelper.CreateVehicle(vhcle);
        }

        public IList<Vehicle> GetAll()
        {
            return _vehicleHelper.GetAll();
        }
    }
}
