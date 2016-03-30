using OpenCRM.BizLogic.Helpers.Interfaces;
using OpenCRM.Common.DTO;
using OpenCRM.DB.DomainObjects;
using OpenCRM.DB.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.BizLogic.Helpers.Impl
{
   public class CompanyHelper:ICompanyHelper
    {
       private ICompanyRepo _companyRepo;
       public CompanyHelper(ICompanyRepo companyRepo) 
       {
           _companyRepo = companyRepo;
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
            _companyRepo.Add(compny);

            return new HttpResult("Created Successfully!", true);
        }
    }
}
