using CsvHelper;
using OpenCRM.BusinessManagers.Interfaces;
using OpenCRM.DB.DomainObjects;
using OpenCRM.DB.Repository;
using OpenCRM.Web.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Transactions;
using System.Web;
using System.Web.Http;

namespace OpenCRM.Web.API.admin
{
    [RoutePrefix("api/inbox")]

    public class InboxController : ApiController
    {

        private IInboxManager _inboxManager;


        public InboxController(IInboxManager inboxManager)
        {

            _inboxManager = inboxManager;

        }
        [Route("SendEmail")]
        public void SendEmail(Inbox inbox)
        {
            _inboxManager.SendEmail(inbox);

        }


        [Route("GetEmails")]
        public IList<Inbox> GetEmails(int userId, int companyId)
        {
            return _inboxManager.GetEmails(userId, companyId);
        }

        [Route("FlagEmail")]
        public void FlagEmail(Inbox inbox)
        {
            _inboxManager.FlagEmail(inbox);
        }

        [Route("ReadEmail")]
        public void ReadEmail(Inbox inbox)
        {
            _inboxManager.ReadEmail(inbox);
        }

        [HttpPost]
        [Route("DeleteEmails")]
        public void DeleteEmails(string[] inbox)
        {
            _inboxManager.DeleteEmails(inbox);

        }

        [Route("GetEmailBySenderId")]
        public Inbox GetEmailBySenderId(int senderId)
        {
            return _inboxManager.GetEmailBySenderId(senderId);

        }
    }
}