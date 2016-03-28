using OpenCRM.DB.DomainObjects;
using NHibernate;
using NHibernate.Transform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Linq;
namespace OpenCRM.DB.Repository
{
    public class DealerShipRepo : Repository<DealerShip>, IDealerShipRepo
    {
        private readonly ISession _session;
        public DealerShipRepo(ISession session)
            : base(session)
        {
            _session = session;
        }
    }
}