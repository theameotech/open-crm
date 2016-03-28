using OpenCRM.DB.DomainObjects;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.DB.Repository
{
    public class CompanyRepo : Repository<Company>, ICompanyRepo
    {
        public CompanyRepo(ISession session)
            : base(session)
        { }
    }
}
