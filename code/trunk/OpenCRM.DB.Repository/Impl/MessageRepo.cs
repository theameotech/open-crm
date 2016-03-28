using OpenCRM.DB.DomainObjects;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.DB.Repository
{
    public class MessageRepo : Repository<Message>, IMessageRepo
    {
        public MessageRepo(ISession session)
            : base(session)
        { }
    }
}
