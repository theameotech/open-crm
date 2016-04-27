using OpenCRM.DB.DomainObjects;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenCRM.DB.Mapping
{
    public class DoListMap : ClassMap<DoList>
    {
        public DoListMap()
        {
            Table("tbl_dolist");
            Id(x => x.DoListID).Not.Nullable();
            Map(x => x.UserID).Not.Nullable();
            Map(x => x.CompanyID).Not.Nullable();
            Map(x => x.ListPriority).Not.Nullable();
            Map(x => x.ListMessage).Not.Nullable();
            Map(x => x.CreateDate).Not.Nullable();
            Map(x => x.ListCategory);
            Map(x => x.TargetDate);
            Map(x => x.IsRead);
            Map(x => x.IsDelete);
            Map(x => x.IsActive);
            Map(x => x.IsAcheived);
            Map(x => x.SystemDate);
            Map(x => x.ServerDate);
            Map(x => x.CreateTime).Not.Nullable();
            Map(x => x.LastUpdateTime);
            Map(x => x.CompletionDate);
            Map(x => x.IsCompleted);
         
        }
    }
}

