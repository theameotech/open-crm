using OpenCRM.Common.DTO;
using OpenCRM.DB.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.BizLogic.Helpers.Interfaces
{
 public  interface ICompanyHelper
    {
     HttpResult AddCompany(Company company);
     Company GetCompanyById(int userId);

    IList<Company> GetAllCompany();

     void BlockCompany(int companyId);

     void UnblockCompany(int companyId);
     Company GetCompanyByCompanyId(int companyId);
     LoginResult login(Company company);
     Company IsVerify(int companyId);
     IList<User> GetCompanyUser(int companyId);
    }
}
