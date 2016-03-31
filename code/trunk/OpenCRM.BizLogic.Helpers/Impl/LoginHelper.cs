using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCRM.Common.DTO;
using OpenCRM.DB.DomainObjects;
using OpenCRM.BizLogic.Helpers.Interfaces;
using OpenCRM.DB.Repository;

namespace OpenCRM.BizLogic.Helpers.Impl
{
    public class LoginHelper : ILoginHelper
    {
        private IUserRepo _userRepo;
        private IUserRoleRepo _userrolesRepo;
        private IRoleRepo _rolesRepo;
        public LoginHelper(IUserRepo userRepo, IUserRoleRepo userrolesRepo, IRoleRepo rolesRepo)
        {
            _userRepo = userRepo;
            _userrolesRepo = userrolesRepo;
            _rolesRepo = rolesRepo;
        }

        public void Logout(User user)
        {
            var dbUser = _userRepo.Get(x => x.AuthToken == user.AuthToken);
            if (dbUser != null)
            {
                dbUser.AuthToken = "";
                _userRepo.Update(dbUser);

            }
        }

        public LoginResult login(User user)
        {
            try
            {
                var dbUser = _userRepo.Get(x => x.UserName == user.UserName
                    && x.Password == user.Password);

                string token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

                if (dbUser != null)
                {
                        dbUser.AuthToken = token;
                        _userRepo.Update(dbUser);
                       
                    return new LoginResult
                    {
                        Success = true,
                        Token = token,
                        IsAdmin = IsAdmin(user.UserName),
                        UserId = dbUser.Id,
                        CompanyId = dbUser.CompanyId,
                        FirstName = dbUser.FirstName,
                        LastName = dbUser.LastName,
                        CreateTime=dbUser.CreateTime

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
