using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenCRM.Common.DTO
{
    public class LoginResult
    {
        public string Token { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public bool IsAdmin { get; set; }
        public long UserId { get; set; }
        public int CompanyId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string UserPrivilege { get; set; }
        public bool Isblock { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CompanyName { get; set; }
        public string BussinessEmail { get; set; }
        public DateTime CreateTime { get; set; }
    }
}