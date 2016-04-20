using OpenCRM.Common.DTO;
using OpenCRM.DB.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.BusinessManagers.Interfaces
{
    public interface IDoListManager
    {
        void AddDoList(DoList dolist);
        IList<DoList> GetAllList();
        DoList GetDoList(int dolistId);
        void DeleteDolist(int dolistId);
        DoList IsRead(int dolistId);
        void CompleteTask(int dolistId);
    }
}
