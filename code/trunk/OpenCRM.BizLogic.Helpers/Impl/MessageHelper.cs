using OpenCRM.DB.DomainObjects;
using OpenCRM.DB.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenCRM.Web.Helpers
{
    public class MessageHelper : IMessageHelper
    {
        private IMessageRepo _messageRepo;
        private IUserRepo _userRepo;
        public MessageHelper(IMessageRepo messageRepo, IUserRepo userRepo)
        {
            _messageRepo = messageRepo;
            _userRepo = userRepo;
        }



        public void SendMessage(Message message)
        {
            
                Message msg = new Message();
                msg.CompanyID = message.CompanyID;
                msg.MessageID = message.MessageID;
                msg.UserID = message.UserID;
                msg.ToUserID = message.ToUserID;
                msg.MessageBody = message.MessageBody;
                msg.UserName = message.UserName;
                var user = _userRepo.Get(x => x.Id == message.ToUserID);
                msg.ToUserName = user.FirstName;
                //msg.TimeStamp = message.TimeStamp;
                _messageRepo.Add(msg);
               
        }


        public IList<Message> GetMessages(int lastMessageId, int userId, int toUserId)
        {
            return _messageRepo.FetchAll(x => x.MessageID > lastMessageId &&
               ((x.UserID == userId && x.ToUserID == toUserId) || (x.UserID == toUserId && x.ToUserID == userId)));
        }
    }
}