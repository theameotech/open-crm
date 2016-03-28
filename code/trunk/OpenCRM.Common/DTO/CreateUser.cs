using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenCRM.Common.DTO
{
    public class CreateUser
    {
        public CreateUser(string message, bool result)
        {
            Success = result;
            Message = message;
        }
        public bool Success { get; set; }
        public string Message { get; set; }
      
    }
}