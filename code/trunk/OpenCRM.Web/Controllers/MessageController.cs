using AC.Dto;
using AC.Filters;
using AC.helpers;
using AC.Models;
using AC.Repository;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Transactions;
using System.Web;
using System.Web.Http;

namespace OpenCRM.Web.Controllers.admin
{
    [RoutePrefix("api/message")]
    [AuthorizedUser]
    public class MessageController : ApiController
    {

        private IMessageRepo _messageRepo;
        private IMessageHelper _messageHelper;

        public MessageController(IMessageRepo messageRepo, IMessageHelper messageHelper)
        {

            _messageRepo = messageRepo;
            _messageHelper = messageHelper;

        }
         [Route("SendMessage")]
        public void SendMessage(Message message) 
        {
            _messageHelper.SendMessage(message);

        }
        [Route("GetMessages")]
         public IList<Message> GetMessages(int lastMessageId,int userId,int toUserId) 
         {
             return _messageHelper.GetMessages(lastMessageId,userId,toUserId);
         }


    }
}