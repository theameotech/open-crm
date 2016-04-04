using OpenCRM.DB.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.BusinessManagers.Interfaces
{
   public interface IInboxManager
    {

       IList<Inbox> GetEmails(int userId, int companyId);
    }

}
