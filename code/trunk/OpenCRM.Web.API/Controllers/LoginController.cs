using OpenCRM.BusinessManagers.Interfaces;
using OpenCRM.Common.DTO;
using OpenCRM.DB.DomainObjects;
using OpenCRM.DB.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Transactions;
using System.Web.Http;

namespace OpenCRM.Web.API
{
    [RoutePrefix("api/user")]
    public class LoginController : ApiController
    {
       
        private ILoginManager _loginManager;
        public LoginController(ILoginManager loginManager)
        {
            _loginManager = loginManager;
        }

        [Route("logout")]
        public void Logout(User user)
        {
            _loginManager.Logout(user);
        }

        [Route("login")]
        public LoginResult login(User user)
        {
            return _loginManager.login(user);
        }
          [Route("IsAdmin")]
        public bool IsAdmin(string username)
        {
            return _loginManager.IsAdmin(username);
        }

    }
}
