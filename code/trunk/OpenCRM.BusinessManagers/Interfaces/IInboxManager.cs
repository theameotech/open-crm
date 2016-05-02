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
       void SendEmail(Inbox inbox);
       void FlagEmail(Inbox inbox);
       void DeleteEmails(string[] inbox);
       Inbox GetEmailBySenderId(int senderId);
       void ReadEmail(Inbox inbox);
    }

}
