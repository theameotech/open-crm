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

        private IInboxHelper _inboxHelper;
        public InboxManager(IInboxHelper inbocHelper)
        {
            _inboxHelper = inbocHelper;
        }

        public IList<Inbox> GetEmails(int userId, int companyId)
        {
            return _inboxHelper.GetEmails(userId, companyId);
        }

        public void SendEmail(Inbox inbox)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                _inboxHelper.SendEmail(inbox);
                scope.Complete();
            }
        }

        public void FlagEmail(Inbox inbox)
        {

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                _inboxHelper.FlagEmail(inbox);
                scope.Complete();
            }

        }

        public void DeleteEmails(string[] inbox)
        {


            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                _inboxHelper.DeleteEmails(inbox);
                scope.Complete();
            }
        }
        public Inbox GetEmailBySenderId(int senderId)
        {
            return _inboxHelper.GetEmailBySenderId(senderId);
        }


        public void ReadEmail(Inbox inbox)
        {
            _inboxHelper.ReadEmail(inbox);
        }
    }
}
