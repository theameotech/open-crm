using NHibernate;
using OpenCRM.DB.DomainObjects;
using OpenCRM.DB.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.DB.Repository.Impl
{
    public class InboxRepo : Repository<Inbox>, IInboxRepo
    {

        public InboxRepo(ISession session)
            : base(session)
        { }
    }
}
