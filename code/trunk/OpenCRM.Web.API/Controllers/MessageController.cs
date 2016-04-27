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
    [RoutePrefix("api/message")]
    public class MessageController : ApiController
    {

        private IMessageManager _messageManager;
      

        public MessageController(IMessageManager messageManager)
        {

            _messageManager = messageManager;

        }
         [Route("SendMessage")]
        public void SendMessage(Message message) 
        {
            _messageManager.SendMessage(message);

        }
        [Route("GetMessages")]
         public IList<Message> GetMessages(int lastMessageId,int userId,int toUserId) 
         {
             return _messageManager.GetMessages(lastMessageId, userId, toUserId);
         }


    }
}