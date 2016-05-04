using OpenCRM.Common.DTO;
using OpenCRM.DB.DomainObjects;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.BizLogic.Helpers.Interfaces
{
    public interface IEmailTemplateHelper
    {
        void AddEmailTemplate(EmailTemplate emailtemplate);
        IList<EmailTemplate> GetAllTemplate();
        EmailTemplate GetEmailTemplate(int EmailtemplateID);


    }
}
