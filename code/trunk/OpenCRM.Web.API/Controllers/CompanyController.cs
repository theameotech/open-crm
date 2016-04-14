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


    }
}
