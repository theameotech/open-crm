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
    public class DoListManager : IDoListManager
    {
        private IDoListHelper _doListHelper;
        public DoListManager(IDoListHelper doListHelper)
        {
            _doListHelper = doListHelper;
        }




        public void AddDoList(DoList dolist)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    _doListHelper.AddDoList(dolist);
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public IList<DoList> GetAllList()
        {
            return _doListHelper.GetAllList();
        }


        public DoList GetDoList(int dolistId)
        {
            return _doListHelper.GetDoList(dolistId);
        }


        public void DeleteDolist(int dolistId)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    _doListHelper.DeleteDolist(dolistId);
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public DoList IsRead(int dolistId)
        {
            var newlist = new DoList();
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {

                try
                {
                    newlist = _doListHelper.IsRead(dolistId);
                    scope.Complete();


                }
                catch (Exception ex)
                {

                    scope.Dispose();
                    throw ex;
                }
            }
            return newlist;
        }


        public void CompleteTask(int dolistId)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {

                try
                {
                    _doListHelper.CompleteTask(dolistId);
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}