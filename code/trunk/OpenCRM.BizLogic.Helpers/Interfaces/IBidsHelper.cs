using OpenCRM.DB.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.BizLogic.Helpers.Interfaces
{
   public interface IBidsHelper
    {

       void CreateBid(Bids bids);

       IList<Bids> GetAll();
    }
}
