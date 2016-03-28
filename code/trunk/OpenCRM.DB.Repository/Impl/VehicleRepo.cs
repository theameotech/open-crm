using OpenCRM.DB.DomainObjects;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.DB.Repository
{
    public class VehicleRepo : Repository<Vehicle>, IVehicleRepo
    {
        public VehicleRepo(ISession session)
            : base(session)
        { }
    }
}
