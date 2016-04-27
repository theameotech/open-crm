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
    [RoutePrefix("api/company")]
    public class CompanyController : ApiController
    {
        private ICompanyManager _companyManager;
        public CompanyController(ICompanyManager companyManager)
        {
            _companyManager = companyManager;
        }


        [Route("AddCompany")]
        public HttpResult AddCompany(Company company)
        {
            return _companyManager.AddCompany(company);

        }
        [Route("GetCompanieById")]
        public Company GetCompanieById(int userId)
        {
            return _companyManager.GetCompanieById(userId);
        }

        [Route("GetAllCompany")]
        public IList<Company> GetAllCompany()
        {
            return _companyManager.GetAllCompany();

        }

        [HttpGet]
        [Route("BlockCompany")]
        public void BlockCompany(int companyId)
        {
            _companyManager.BlockCompany(companyId);
        }
        [HttpGet]
        [Route("UnblockCompany")]
        public void UnblockCompany(int companyId)
        {
            _companyManager.UnblockCompany(companyId);
        }
        [Route("GetCompanyByCompanyId")]
        public Company GetCompanyByCompanyId(int companyId)
        {
            return _companyManager.GetCompanyByCompanyId(companyId);
        }
        [Route("login")]
        public LoginResult login(Company company)
        {
            return _companyManager.login(company);
        }
        [HttpGet]
        [Route("IsVerify")]
        public Company IsVerify(int companyId)
        {
            return _companyManager.IsVerify(companyId);
        }
    }
}
