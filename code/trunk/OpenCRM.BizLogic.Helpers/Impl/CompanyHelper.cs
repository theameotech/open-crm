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
    public class CompanyHelper : ICompanyHelper
    {
        private ICompanyRepo _companyRepo;
        private IUserRepo _userRepo;
        public CompanyHelper(ICompanyRepo companyRepo, IUserRepo userRepo)
        {
            _companyRepo = companyRepo;
            _userRepo = userRepo;
        }
        public HttpResult AddCompany(Company company)
        {
            if (company.CompanyID >= 0)
            {
                if (_companyRepo.IsExist(x => x.CompanyAdmin == company.CompanyAdmin && x.CompanyID != company.CompanyID))
                {
                    throw new AlreadyExistException(string.Format("{0} already exist.", company.CompanyAdmin));
                }

                if (_companyRepo.IsExist(x => x.BusinessEmail == company.BusinessEmail && x.CompanyID != company.CompanyID))
                {
                    throw new AlreadyExistException(string.Format("{0} already exist.", company.BusinessEmail));
                }




            }
            else
            {
                if (_companyRepo.IsExist(x => x.CompanyName == company.CompanyName))
                {
                    throw new AlreadyExistException(string.Format("{0} already exist.", company.CompanyName));
                }
                if (_companyRepo.IsExist(x => x.BusinessEmail == company.BusinessEmail))
                {
                    throw new AlreadyExistException(string.Format("{0} already exist.", company.BusinessEmail));
                }

            }
            Company compny = new Company();
            compny.CompanyName = company.CompanyName;
            compny.BusinessEmail = company.BusinessEmail;
            compny.CompanyType = company.CompanyType;
            compny.CompanyAdmin = company.CompanyAdmin;
            compny.AdminPassword = company.AdminPassword;
            compny.CompanyPhone = company.CompanyPhone;
            compny.CompanyAddress = company.CompanyAddress;
            compny.CompanyCity = company.CompanyCity;
            compny.CompanyState = company.CompanyState;
            compny.CompanyZipCode = company.CompanyZipCode;
            compny.CompanyCountry = company.CompanyCountry;
            compny.ServerDate = DateTime.Now;
            compny.SystemDate = DateTime.Now;


            _companyRepo.Add(compny);
            SendGridMessage myMessage = new SendGridMessage();
            myMessage.AddTo(compny.BusinessEmail);
            myMessage.From = new MailAddress("Support@openCrm.com", "OpenCRM Supprot");
            myMessage.Subject = "Thanks From OPEN CRM";
            myMessage.Html = "'Thanks for become a member of us your UserName is:'"+"<b>"+ compny.CompanyAdmin +"</b>"+"'and Password is:'"+"<b>"+compny.AdminPassword +"</b>"+"' Please click on this Url  http://localhost:26159/app/index.html#/companylogin'";
           
            // Create a Web transport, using API Key
            var transportWeb = new SendGrid.Web("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");
            transportWeb.DeliverAsync(myMessage);

            return new HttpResult("Created Successfully!", true);
        }

        public Company IsVerify(int companyId)
        {
            var company = _companyRepo.Get(x => x.CompanyID == companyId);

            company.IsVerify = true;
            _companyRepo.Update(company);
            return company;
        }





        public Company GetCompanyById(int userId)
        {
            var user = _userRepo.Get(x => x.Id == userId);
            var company = _companyRepo.Get(x => x.CompanyID == user.CompanyID);
            return company;
        }


        public IList<Company> GetAllCompany()
        {
            return _companyRepo.GetAll();
        }


        public void BlockCompany(int companyId)
        {
            var company = _companyRepo.Get(x => x.CompanyID == companyId);

            company.IsBlock = true;

            _companyRepo.Update(company);
            var comapnyUser = _userRepo.FetchAll(x => x.CompanyID == companyId).ToList();

            foreach (var item in comapnyUser)
            {
                item.Isblock = true;
                _userRepo.Update(item);
            }

        }


        public void UnblockCompany(int companyId)
        {
            var company = _companyRepo.Get(x => x.CompanyID == companyId);

            company.IsBlock = false;

            _companyRepo.Update(company);
            var comapnyUser = _userRepo.FetchAll(x => x.CompanyID == companyId).ToList();

            foreach (var item in comapnyUser)
            {
                item.Isblock = false;
                _userRepo.Update(item);
            }
        }

        public Company GetCompanyByCompanyId(int companyId)
        {
            return _companyRepo.Get(x => x.CompanyID == companyId);
        }

        public LoginResult login(Company company)
        {


            try
            {
                var dbcompany = _companyRepo.Get(x => x.CompanyAdmin == company.CompanyAdmin && x.AdminPassword == company.AdminPassword);


                //string token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

                if (dbcompany != null)
                {

                    return new LoginResult
                    {
                        Success = true,
                        CompanyId = dbcompany.CompanyID,
                        UserName = dbcompany.CompanyAdmin,
                        Password = dbcompany.AdminPassword,
                        CompanyName = dbcompany.CompanyName,
                        BussinessEmail = dbcompany.BusinessEmail
                 

                    };
                }
                return new LoginResult
                {
                    Success = false,
                };
            }
            catch (Exception ex)
            {
                return new LoginResult
                {
                    Success = false,
                    Message = ex.InnerException.Message
                };
            }
        }


        public IList<User> GetCompanyUser(int companyId)
        {
            return _userRepo.FetchAll(x => x.CompanyID == companyId);
        }
    }
}
