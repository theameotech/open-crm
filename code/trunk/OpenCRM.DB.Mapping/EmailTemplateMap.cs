using OpenCRM.DB.DomainObjects;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenCRM.DB.Mapping
{
    public class EmailTemplateMap : ClassMap<EmailTemplate>
    {
        public EmailTemplateMap()
        {
            Table("tbl_emailtemplate");
            Id(x => x.EmailTemplateID).Not.Nullable();
            Map(x => x.EmailTemplateName).Not.Nullable();
            Map(x => x.EmailTemplateBody).Not.Nullable();
            Map(x => x.CreateDate).Not.Nullable();
            Map(x => x.ModifiedDate);
            Map(x => x.IsActive);
            Map(x => x.IsDelete);
            Map(x => x.SystemDate);
            Map(x => x.ServerDate);
        }
    }
}
