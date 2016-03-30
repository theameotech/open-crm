using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenCRM.DB.DomainObjects
{
    public class Company : BaseDomainObject
    {
        public virtual int CompanyID { get; set; }
        public virtual string CompanyName { get; set; }
        public virtual string BusinessEmail { get; set; }
        public virtual string CompanyType { get; set; }
        public virtual string CompanyAdmin { get; set; }
        public virtual string AdminPassword { get; set; }
        public virtual string CompanyPhone { get; set; }
        public virtual string CompanyAddress { get; set; }
        public virtual string CompanyCity { get; set; }
        public virtual string CompanyState { get; set; }
        public virtual string CompanyZipCode { get; set; }
        public virtual string CompanyCountry { get; set; }
        public virtual bool IsVerify { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual bool IsBlock { get; set; }
        public virtual bool IsApproved { get; set; }
        //public virtual DateTime CreateTime { get; set; }
        //public virtual DateTime LastUpdateTime { get; set; }
        public virtual DateTime SystemDate { get; set; }
        public virtual DateTime ServerDate { get; set; }





    }
}