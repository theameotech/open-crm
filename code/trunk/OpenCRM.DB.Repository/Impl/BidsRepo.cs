using OpenCRM.DB.DomainObjects;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.DB.Repository
{
    public class BidsRepo : Repository<Bids>, IBidsRepo
    {
        public BidsRepo(ISession session)
            : base(session)
        { }
    }
}
