using OpenCRM.BizLogic.Helpers.Interfaces;
using OpenCRM.Common.DTO;
using OpenCRM.DB.DomainObjects;
using OpenCRM.DB.Repository;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.BizLogic.Helpers.Impl
{
    public class UserHelper : IUserHelper
    {

        private IUserRepo _userRepo;
        private IRoleRepo _rolesRepo;
        private IUserRoleRepo _userrolesRepo;
        private ICompanyRepo _companyRepo;

        public UserHelper(IUserRepo userRepo, IUserRoleRepo userrolesRepo, IRoleRepo rolesRepo, ICompanyRepo companyRepo)
        {

            _userRepo = userRepo;
            _userrolesRepo = userrolesRepo;
            _rolesRepo = rolesRepo;
            _companyRepo = companyRepo;
        }

        public HttpResult AddUser(UserDTO users)
        {


            if (users.User.Id > 0)
            {
                if (_userRepo.IsExist(x => x.UserName == users.User.UserName && x.Id != users.User.Id && x.Deleted == false))
                {
                    throw new AlreadyExistException(string.Format("{0} already exist.", users.User.UserName));
                }

                if (_userRepo.IsExist(x => x.UserEmail == users.User.UserEmail && x.Id != users.User.Id && x.Deleted == false))
                {
                    throw new AlreadyExistException(string.Format("{0} already exist.", users.User.UserEmail));
                }

            }
            else
            {
                if (_userRepo.IsExist(x => x.UserName == users.User.UserName && x.Deleted == false))
                {
                    throw new AlreadyExistException(string.Format("{0} already exist.", users.User.UserName));
                }
                if (_userRepo.IsExist(x => x.UserEmail == users.User.UserEmail && x.Deleted == false))
                {
                    throw new AlreadyExistException(string.Format("{0} already exist.", users.User.UserEmail));
                }


            }
            if (users.User.Id > 0)
            {
                var user = _userRepo.Get(x => x.Id == users.User.Id);
                user.UserName = users.User.UserName;
                user.UserPassword = users.User.UserPassword;
                user.UserEmail = users.User.UserEmail;
                user.UserPhone = users.User.UserPhone;
                user.FirstName = users.User.FirstName;
                user.LastName = users.User.LastName;
                user.UserOfficePhoneExt = users.User.UserOfficePhoneExt;
                user.UserAddress = users.User.UserAddress;
                user.UserAlternateAddress = users.User.UserAlternateAddress;
                user.UserCity = users.User.UserCity;
                user.UserCountry = users.User.UserCountry;
                user.UserState = users.User.UserState;
                user.UserZipCode = users.User.UserZipCode;
                user.Isblock = users.User.Isblock;
                user.IsVerify = users.User.IsVerify;
                user.IsActive = users.User.IsActive;
                user.Gender = users.User.Gender;
                user.UserPrivilege = users.User.UserPrivilege;

                if (user.CompanyID > 0)
                {
                    _companyRepo.Delete(_companyRepo.Get(x => x.CompanyID == user.CompanyID));
                }
                Company cmpny = new Company();
                cmpny.CompanyName = users.CompanyName;
                _companyRepo.Add(cmpny);
                user.CompanyID = cmpny.CompanyID;

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


            }


            else
            {

                User user = new User();
                user.UserName = users.User.UserName;
                user.UserPassword = users.User.UserPassword;
                user.UserEmail = users.User.UserEmail;
                user.UserPhone = users.User.UserPhone;
                user.FirstName = users.User.FirstName;
                user.LastName = users.User.LastName;
                user.UserOfficePhoneExt = users.User.UserOfficePhoneExt;
                user.UserAddress = users.User.UserAddress;
                user.UserAlternateAddress = users.User.UserAlternateAddress;
                user.UserCity = users.User.UserCity;
                user.UserCountry = users.User.UserCountry;
                user.UserState = users.User.UserState;
                user.UserZipCode = users.User.UserZipCode;
                user.Isblock = users.User.Isblock;
                user.IsVerify = users.User.IsVerify;
                user.IsActive = users.User.IsActive;
                user.Gender = users.User.Gender;
                user.UserPrivilege = "Master";



                Company cmpny = new Company();
                cmpny.CompanyName = users.CompanyName;
                _companyRepo.Add(cmpny);
                user.CompanyID = cmpny.CompanyID;
                _userRepo.Add(user);

                foreach (var roles in users.Roles)
                {
                    UserRoles role = new UserRoles();
                    role.UserId = user.Id;
                    role.RoleId = roles.Id;
                    _userrolesRepo.Add(role);

                }


            }
            return new HttpResult("Created Successfully!", true);
        }


        public User GetUser(int userId)
        {
            return _userRepo.Get(x => x.Id == userId);
        }

        public IList<User> GetAll()
        {
            return _userRepo.FetchAll(x => x.Deleted == false);

        }

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

        public void DeleteUser(int userId)
        {

            var user = _userRepo.Get(x => x.Id == userId);
            user.Deleted = true;
            _userRepo.Update(user);


        }


        public void UpdatePassword(User user)
        {
            var username = _userRepo.Get(x => x.UserName == user.UserName);
            username.UserPassword = user.UserPassword;
            SendGridMessage myMessage = new SendGridMessage();
            myMessage.AddTo(username.UserEmail);
            myMessage.From = new MailAddress("Support@openCrm.com", "OpenCRM Supprot");
            myMessage.Subject = "Testing the SendGrid Library";
            myMessage.Text = "Hi your new Password is : " + user.UserPassword + " please login with this password";
            // Create a Web transport, using API Key
            var transportWeb = new SendGrid.Web("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");
            transportWeb.DeliverAsync(myMessage);

            _userRepo.Update(username);
        }
    }

}
