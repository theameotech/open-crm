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
   public class CompanyHelper:ICompanyHelper
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
                if (_companyRepo.IsExist(x => x.CompanyAdmin == company.CompanyAdmin && x.CompanyID != company.CompanyID ))
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
                if (_companyRepo.IsExist(x => x.CompanyName == company.CompanyName ))
                {
                    throw new AlreadyExistException(string.Format("{0} already exist.",company.CompanyName));
                }
                if (_companyRepo.IsExist(x => x.BusinessEmail == company.BusinessEmail))
                {
                    throw new AlreadyExistException(string.Format("{0} already exist.", company.BusinessEmail));
                }
           
            }
            Company compny = new Company();
            compny.CompanyName=company.CompanyName;
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
            company.ServerDate = DateTime.Now;
            company.SystemDate = DateTime.Now;
            SendGridMessage myMessage = new SendGridMessage();
            myMessage.AddTo(compny.BusinessEmail);
            myMessage.From = new MailAddress("Support@openCrm.com", "OpenCRM Supprot");
            myMessage.Subject = "Testing the SendGrid Library";
            myMessage.Text = "Hi Welcome to OpenCRM";
            // Create a Web transport, using API Key
            var transportWeb = new SendGrid.Web("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");
            transportWeb.DeliverAsync(myMessage);
            _companyRepo.Add(compny);

            return new HttpResult("Created Successfully!", true);
        }





       public Company GetCompanieById(int userId)
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
    }
}
