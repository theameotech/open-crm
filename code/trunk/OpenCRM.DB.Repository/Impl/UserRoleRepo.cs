using NHibernate;
using OpenCRM.DB.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.DB.Repository
{
    public class UserRoleRepo : Repository<UserRoles>, IUserRoleRepo
    {
        public UserRoleRepo(ISession session)
            : base(session)
        { }
    }
}
