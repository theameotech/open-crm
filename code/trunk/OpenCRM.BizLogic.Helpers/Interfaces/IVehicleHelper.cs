using OpenCRM.DB.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.BizLogic.Helpers.Interfaces
{
   public interface IVehicleHelper
    {

       void CreateVehicle(Vehicle vhcle);

       IList<Vehicle> GetAll();
    }
}
