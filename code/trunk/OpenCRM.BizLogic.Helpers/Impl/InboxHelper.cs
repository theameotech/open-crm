using OpenCRM.BizLogic.Helpers.Interfaces;
using OpenCRM.DB.DomainObjects;
using OpenCRM.DB.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.BizLogic.Helpers.Impl
{
    public class InboxHelper : IInboxHelper
    {

        private IInboxRepo _inboxRepo;

        public InboxHelper(IInboxRepo inboxRepo) 
        {
            _inboxRepo = inboxRepo;
        }
        public IList<Inbox> GetEmails(int userId, int companyId)
        {
            return _inboxRepo.FetchAll(x => x.UserID == userId && x.CompanyID == companyId);
        }
    }
}
