using OpenCRM.BizLogic.Helpers.Interfaces;
using OpenCRM.BusinessManagers.Interfaces;
using OpenCRM.Common.DTO;
using OpenCRM.DB.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace OpenCRM.BusinessManagers.Impl
{
    public class CompanyManager : ICompanyManager
    {
        private ICompanyHelper _companyHelper;
        public CompanyManager(ICompanyHelper companyHelper)
        {
            _companyHelper = companyHelper;
        }
        public HttpResult AddCompany(Company company)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    _companyHelper.AddCompany(company);
                    scope.Complete();
                }
                catch (AlreadyExistException aex)
                {
                    return new HttpResult(aex.Message, false);
                }
                catch (Exception ex)
                {
                    return new HttpResult(ex.Message, false);
                }
            }
            return new HttpResult("Created Successfully!", true);
        }





        public Company GetCompanieById(int userId)
        {

          return  _companyHelper.GetCompanieById(userId);
        }


        public IList<Company> GetAllCompany()
        {
            return _companyHelper.GetAllCompany();
        }


        public void BlockCompany(int companyId)
        {

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                _companyHelper.BlockCompany(companyId);
                scope.Complete();
            }
            
        }
    }
}
