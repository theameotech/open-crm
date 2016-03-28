using AC.Dto;
using AC.Models;
using AC.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Transactions;
using System.Web.Http;

namespace OpenCRM.Web.Controllers
{
    [RoutePrefix("api/user")]
    public class LoginController : ApiController
    {
        private IRepository<User> _userRepo;
        private IRepository<UserRoles> _userrolesRepo;
        private IRepository<Roles> _rolesRepo;
        public LoginController(IRepository<User> userRepo, IRepository<UserRoles> userrolesRepo, IRepository<Roles> rolesRepo)
        {
            _userRepo = userRepo;
            _userrolesRepo = userrolesRepo;
            _rolesRepo = rolesRepo;
        }

        [Route("logout")]
        public void Logout(User user)
        {
            var dbUser = _userRepo.Get(x => x.AuthToken == user.AuthToken);
            if (dbUser != null)
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    dbUser.AuthToken = "";
                    _userRepo.Update(dbUser);
                    scope.Complete();
                }
            }
        }

        [Route("login")]
        public LoginResult login(User user)
        {
            try
            {
                var dbUser = _userRepo.Get(x => x.UserName == user.UserName
                    && x.Password == user.Password );

                string token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

                if (dbUser != null)
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        dbUser.AuthToken = token;
                        _userRepo.Update(dbUser);
                        scope.Complete();
                    }

                    return new LoginResult
                    {
                        Success = true,
                        Token = token,
                        IsAdmin = IsAdmin(user.UserName),
                        UserId=dbUser.Id,
                        CompanyId=dbUser.CompanyId,
                        FirstName=dbUser.FirstName,
                        LastName=dbUser.LastName

                    };
                }
                return new LoginResult
                {
                    Success = false,
                    Token = ""
                };
            }
            catch (Exception ex)
            {
                return new LoginResult
                {
                    Success = false,
                    Token = "",
                    Message = ex.InnerException.Message
                };
            }
        }
          [Route("IsAdmin")]
        public bool IsAdmin(string username)
        {
            var isAdmin = false;
            var user = _userRepo.Get(x => x.UserName == username);
            var userRole = _userrolesRepo.FetchAll(x => x.UserId == user.Id);
            foreach (var role in userRole)
            {
                var rl = _rolesRepo.Get(x => x.Id == role.RoleId);
                if (rl.Name == "OpenCRM ADMIN")
                {
                    isAdmin = true;
                    break;
                }
            }
            return isAdmin;
        }

    }
}
