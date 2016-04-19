using CsvHelper;
using OpenCRM.BusinessManagers.Interfaces;
using OpenCRM.Common.DTO;
using OpenCRM.DB.DomainObjects;
using OpenCRM.DB.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Transactions;
using System.Web;
using System.Web.Http;

namespace OpenCRM.Web.API.admin
{
    [RoutePrefix("api/admin/user")]
    public class UserController : ApiController
    {

        private IUserManager _userManager;

        public UserController(IUserManager userManager)
        {

            _userManager = userManager;
        }

        [Route("AddUser")]
        public HttpResult AddUser(UserDTO users) 
        {
         return _userManager.AddUser(users);
        }
        [Route("GetUser")]
        public User GetUser(int userId)
        {
            return _userManager.GetUser(userId);
        }

        [Route("All")]
        public IList<User> GetAll()
        {
            return _userManager.GetAll();
             
        }
        [Route("GetUserRole")]
        public IList<Roles> GetUserRole(int userId)
        {
            return _userManager.GetUserRole(userId);
        }
        [Route("DeleteUser")]
        [System.Web.Http.HttpGet]
        public void DeleteUser(int userId)
        {
            _userManager.DeleteUser(userId);
        }
         [Route("UpdatePassword")]
        public void UpdatePassword(User users)
        {
             _userManager.UpdatePassword(users);
        }

    }
}
