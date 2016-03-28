using OpenCRM.BizLogic.Helpers.Interfaces;
using OpenCRM.DB.DomainObjects;
using OpenCRM.DB.Repository;
using OpenCRM.DB.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.BizLogic.Helpers.Impl
{
   public class DealerShipHelper: IDealerShipHelper
    {
       private IDealerShipRepo _dealerShipRepo;
       public DealerShipHelper(IDealerShipRepo dealerShipRepo) 
       {
           _dealerShipRepo = dealerShipRepo;
       }

        public Guid AddDealerShip(DealerShip dealerShip)
        {
            dealerShip.DealerShipUniqueId = Guid.NewGuid();
            dealerShip.Password = string.Join("", MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(dealerShip.Password)).Select(s => s.ToString("x2")));
            _dealerShipRepo.Add(dealerShip);
            return dealerShip.DealerShipUniqueId;
        }
    }
}
