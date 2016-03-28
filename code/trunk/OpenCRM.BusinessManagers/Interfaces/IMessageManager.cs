using OpenCRM.DB.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.BusinessManagers.Interfaces
{
    public  interface IMessageManager
    {

        void SendMessage(Message message);
        IList<Message> GetMessages(int lastMessageId, int userId, int toUserId);
    }
}
