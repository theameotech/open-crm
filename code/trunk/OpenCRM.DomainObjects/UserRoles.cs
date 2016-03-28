using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenCRM.DB.DomainObjects
{
    public class UserRoles : BaseDomainObject
    {
        public virtual int Id { get; set; }
        public virtual int UserId { get; set; }
        public virtual int RoleId { get; set; }

    }
}