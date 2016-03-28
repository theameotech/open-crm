using OpenCRM.DB.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenCRM.Common.DTO
{
    public class UserDTO
    {
        public User User { get; set; }
        public IList<Roles> Roles { get; set; }
        public string CompanyName { get; set; }
    }
}