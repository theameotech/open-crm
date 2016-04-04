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
  public  class InboxManager:IInboxManager
    {

     private IInboxHelper _inbocHelper;
     public InboxManager(IInboxHelper inbocHelper)
     {
         _inbocHelper = inbocHelper;
     }

        public IList<Inbox> GetEmails(int userId, int companyId)
        {
            return _inbocHelper.GetEmails(userId, companyId);
        }
    }
}
