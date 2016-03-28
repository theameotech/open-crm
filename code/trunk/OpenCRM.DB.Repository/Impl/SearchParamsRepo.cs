using NHibernate;
using OpenCRM.DB.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.DB.Repository
{
    public class SearchParamsRepo : Repository<SearchParams>, ISearchParamsRepo
    {
        public SearchParamsRepo(ISession session)
            : base(session)
        { }
    }
}
