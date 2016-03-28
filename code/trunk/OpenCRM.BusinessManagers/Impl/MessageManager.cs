using OpenCRM.BusinessManagers.Interfaces;
using OpenCRM.DB.DomainObjects;
using OpenCRM.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace OpenCRM.BusinessManagers.Impl
{
   public class MessageManager: IMessageManager
    {

       private IMessageHelper _messageHelper;
       public MessageManager(IMessageHelper messageHelper)
       {
           _messageHelper = messageHelper;
       }

       public void SendMessage(Message message)
       {
           using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
           _messageHelper.SendMessage(message);
                scope.Complete();
            }
       }

       public IList<Message> GetMessages(int lastMessageId, int userId, int toUserId)
       {
           return _messageHelper.GetMessages(lastMessageId, userId, toUserId);
       }
    }
}
