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
    [RoutePrefix("api/dolist")]
    public class DoListController : ApiController
    {

        private IDoListManager _doListManager;

        public DoListController(IDoListManager doListManager)
        {

            _doListManager = doListManager;
        }
        [Route("AddDoList")]
        public void AddDoList(DoList dolist)
        {
            _doListManager.AddDoList(dolist);
        }
        [Route("GetAllList")]
        public IList<DoList> GetAllList()
        {
            return _doListManager.GetAllList();
        }
        [Route("GetDoList")]
        public DoList GetDoList(int dolistId)
        {
            return _doListManager.GetDoList(dolistId);
        }
        [HttpGet]
        [Route("DeleteDolist")]
        public void DeleteDolist(int dolistId)
        {
            _doListManager.DeleteDolist(dolistId);
        }
        [HttpGet]
        [Route("IsRead")]
        public DoList IsRead(int dolistId)
        {
            return _doListManager.IsRead(dolistId);
        }
        [HttpGet]
        [Route("CompleteTask")]
        public void CompleteTask(int dolistId) 
        {
            _doListManager.CompleteTask(dolistId);
        }

    }

}
