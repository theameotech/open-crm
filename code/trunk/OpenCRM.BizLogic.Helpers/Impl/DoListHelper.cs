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
    public class DoListHelper : IDoListHelper
    {
        private IDoListRepo _doListRepo;
        public DoListHelper(IDoListRepo doListRepo)
        {
            _doListRepo = doListRepo; ;
        }
        public void AddDoList(DoList dolist)
        {
            DoList dolst = new DoList();
            dolst.UserID = dolist.UserID;
            dolst.CompanyID = dolist.CompanyID;
            dolst.ListPriority = dolist.ListPriority;
            dolst.ListMessage = dolist.ListMessage;
            dolst.CreateDate = DateTime.Now;
            dolst.ListCategory = dolist.ListCategory;
            dolst.TargetDate = dolist.TargetDate;
            dolst.IsRead = dolist.IsRead;
            dolst.IsDelete = dolist.IsDelete;
            dolst.IsActive = dolist.IsActive;
            dolst.IsAcheived = dolist.IsAcheived;
            dolst.SystemDate = DateTime.Now;
            dolst.ServerDate = DateTime.Now;
            dolst.IsCompleted = dolist.IsCompleted;
            _doListRepo.Add(dolst);
        }
        public IList<DoList> GetAllList()
        {

            return _doListRepo.FetchAll(x => x.IsDelete == false);

        }

        public DoList GetDoList(int dolistId)
        {
            return _doListRepo.Get(x => x.DoListID == dolistId);
        }

        public void DeleteDolist(int dolistId)
        {
            var list = _doListRepo.Get(x => x.DoListID == dolistId);
            list.IsDelete = true;
            _doListRepo.Update(list);
        }

        public DoList IsRead(int dolistId)
        {
            var list = _doListRepo.Get(x => x.DoListID == dolistId);

            list.IsRead = true;
            _doListRepo.Update(list);
            return list;
        }

        public void CompleteTask(int dolistId)
        {
            var Todolist = _doListRepo.Get(x => x.DoListID == dolistId);

            Todolist.CompletionDate = DateTime.Now;
            Todolist.IsCompleted = true;
            _doListRepo.Update(Todolist);

        }
    }

}
