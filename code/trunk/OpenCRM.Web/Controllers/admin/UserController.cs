using AC.Dto;
using AC.Filters;
using AC.Models;
using AC.Repository;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Transactions;
using System.Web;
using System.Web.Http;

namespace OpenCRM.Web.Controllers.admin
{
    [RoutePrefix("api/admin/user")]
    [AuthorizedUser]
    public class UserController : ApiController
    {

        private IUserRepo _userRepo;
        private IRoleRepo _rolesRepo;
        private IUserRoleRepo _userrolesRepo;
        private ICompanyRepo _companyRepo;
        public UserController(IUserRepo userRepo, IUserRoleRepo userrolesRepo, IRoleRepo rolesRepo,ICompanyRepo companyRepo)
        {

            _userRepo = userRepo;
            _userrolesRepo = userrolesRepo;
            _rolesRepo = rolesRepo;
            _companyRepo = companyRepo;
        }

        [Route("AddUser")]
        public CreateUser AddUser(UserDTO users) 
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {

                    if (users.User.Id > 0)
                    {
                        if (_userRepo.IsExist(x => x.UserName == users.User.UserName && x.Id != users.User.Id && x.Deleted==false))
                        {
                            throw new AlreadyExistException(string.Format("{0} already exist.", users.User.UserName));
                        }

                        if (_userRepo.IsExist(x => x.Email == users.User.Email && x.Id != users.User.Id && x.Deleted==false))
                        {
                            throw new AlreadyExistException(string.Format("{0} already exist.", users.User.Email));
                        }

                    }
                    else
                    {
                        if (_userRepo.IsExist(x => x.UserName == users.User.UserName && x.Deleted == false))
                        {
                            throw new AlreadyExistException(string.Format("{0} already exist.", users.User.UserName));
                        }
                        if (_userRepo.IsExist(x => x.Email == users.User.Email && x.Deleted == false))
                        {
                            throw new AlreadyExistException(string.Format("{0} already exist.", users.User.Email));
                        }


                    }
                    if (users.User.Id > 0)
                    {
                        var user = _userRepo.Get(x => x.Id == users.User.Id);
                        user.UserName = users.User.UserName;
                        user.Password = users.User.Password;
                        user.Email = users.User.Email;
                        user.Phone = users.User.Phone;
                        user.FirstName = users.User.FirstName;
                        user.LastName = users.User.LastName;
                        user.Office = users.User.Office;
                        user.StreetAddress1 = users.User.StreetAddress1;
                        user.StreetAddress2 = users.User.StreetAddress2;
                        user.City = users.User.City;
                        user.State = users.User.State;
                        user.Country = users.User.Country;
                        user.PostalCode = users.User.PostalCode;
                        user.EXT = users.User.EXT;

                        if (user.CompanyId > 0) 
                        {
                            _companyRepo.Delete(_companyRepo.Get(x => x.CompanyID == user.CompanyId));
                        }
                        Company cmpny = new Company();
                        cmpny.CompanyName = users.CompanyName;
                        _companyRepo.Add(cmpny);
                        user.CompanyId = cmpny.CompanyID;

                        var userRoles = _userrolesRepo.FetchAll(x => x.UserId == user.Id);
                        foreach (var roles in userRoles)
                        {
                            _userrolesRepo.Delete(roles);
                        }

                        foreach (var roles in users.Roles)
                        {
                            UserRoles role = new UserRoles();
                            role.UserId = user.Id;
                            role.RoleId = roles.Id;
                            _userrolesRepo.Add(role);
                        }

                        _userRepo.Update(user);
                        scope.Complete();

                    }


                    else
                    {

                        User user = new User();
                        user.UserName = users.User.UserName;
                        user.Password = users.User.Password;
                        user.Email = users.User.Email;
                        user.Phone = users.User.Phone;
                        user.FirstName = users.User.FirstName;
                        user.LastName = users.User.LastName;
                        user.Office = users.User.Office;
                        user.StreetAddress1 = users.User.StreetAddress1;
                        user.StreetAddress2 = users.User.StreetAddress2;
                        user.City = users.User.City;
                        user.State = users.User.State;
                        user.Country = users.User.Country;
                        user.PostalCode = users.User.PostalCode;
                        user.EXT = users.User.EXT;
                        Company cmpny = new Company();
                        cmpny.CompanyName = users.CompanyName;
                        _companyRepo.Add(cmpny);
                        user.CompanyId = cmpny.CompanyID;
                        _userRepo.Add(user);

                        foreach (var roles in users.Roles)
                        {
                            UserRoles role = new UserRoles();
                            role.UserId = user.Id;
                            role.RoleId = roles.Id;
                            _userrolesRepo.Add(role);

                        }

                        scope.Complete();
                    }
                    return new CreateUser("Created Successfully!", true);
                }
                catch (AlreadyExistException aex) 
                {
                    return new CreateUser(aex.Message, false);
                
                }


                catch (Exception)
                {
                    scope.Dispose();
                }
            }
            return new CreateUser("Created Successfully!", true);
        }
        [Route("GetUser")]
        public User GetUser(int userId)
        {
            return _userRepo.Get(x => x.Id == userId);
        }

        [Route("All")]
        public IList<User> GetAll()
        {
            return _userRepo.FetchAll(x => x.Deleted == false);
             
        }
        [Route("GetUserRole")]
        public IList<Roles> GetUserRole(int userId)
        {
            var userRoles = _userrolesRepo.FetchAll(x => x.UserId == userId);
            var roles = new List<Roles>();
            foreach (var role in userRoles)
            {
                var rl = _rolesRepo.Get(x => x.Id == role.RoleId);
                roles.Add(rl);
            }
            return roles;
        }
        [Route("DeleteUser")]
        [System.Web.Http.HttpGet]
        public void DeleteUser(int userId)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    var user = _userRepo.Get(x => x.Id == userId);
                    user.Deleted = true;
                    _userRepo.Update(user);
                    scope.Complete();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

    }
}
