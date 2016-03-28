using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCRM.DB.DomainObjects;
using OpenCRM.BusinessManagers.Interfaces;
using OpenCRM.BizLogic.Helpers.Interfaces;

namespace OpenCRM.BusinessManagers.Impl
{
    
     public class DealerShipManager: IDealerShipManager
    {

         private IDealerShipHelper _dealerShipHelper;
        public Guid AddDealerShip(DealerShip dealerShip)
        {
            try
            {
                return _dealerShipHelper.AddDealerShip(dealerShip);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
