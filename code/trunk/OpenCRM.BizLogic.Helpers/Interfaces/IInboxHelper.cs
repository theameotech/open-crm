using OpenCRM.DB.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.BizLogic.Helpers.Interfaces
{
  public  interface IInboxHelper
    {
      IList<Inbox> GetEmails(int userId, int companyId);

      void SendEmail(Inbox inbox);
    }
}
