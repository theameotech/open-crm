using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCRM.DB.DomainObjects;
using OpenCRM.BusinessManagers.Interfaces;
using OpenCRM.BizLogic.Helpers.Interfaces;
using System.Transactions;

namespace OpenCRM.BusinessManagers.Impl
{
    public class InboxManager : IInboxManager
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

        public void SendEmail(Inbox inbox)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                _inbocHelper.SendEmail(inbox);
                scope.Complete();
            }
        }

        public void FlagEmail(Inbox inbox)
        {

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                _inbocHelper.FlagEmail(inbox);
                scope.Complete();
            }

        }

        public void DeleteEmails(string[] inbox)
        {


            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                _inbocHelper.DeleteEmails(inbox);
                scope.Complete();
            }
        }
    }
}
