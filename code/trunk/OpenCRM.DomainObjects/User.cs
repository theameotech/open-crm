using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenCRM.DB.DomainObjects
{
    public class User : BaseDomainObject
    {
        public virtual int Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }

        public virtual string UserName { get; set; }
        public virtual string UserPassword { get; set; }
        public virtual string Gender { get; set; }
        public virtual int CompanyID { get; set; }
        public virtual bool Isblock { get; set; }
        public virtual string UserEmail { get; set; }
        public virtual string UserPhone { get; set; }
        public virtual string UserAddress { get; set; }
        public virtual string UserAlternateAddress { get; set; }
        public virtual string UserCity { get; set; }
        public virtual string UserState { get; set; }
        public virtual string UserZipCode { get; set; }
        public virtual string UserCountry { get; set; }
        public virtual string UserOfficPhone { get; set; }
        public virtual string UserOfficePhoneExt { get; set; }
        public virtual int IsActive { get; set; }
        public virtual int IsVerify { get; set; }
        public virtual bool Deleted { get; set; }
       
        public virtual string AuthToken { get; set; }
        public virtual string UserPrivilege { get; set; }


    }
}