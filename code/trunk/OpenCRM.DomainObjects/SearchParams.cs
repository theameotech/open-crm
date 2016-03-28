using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenCRM.DB.DomainObjects
{
    public class SearchParams: BaseDomainObject
    {
        public virtual int Id { get; set; }
        public virtual int ModelMaxValue { get; set; }
        public virtual int ModelMinValue { get; set; }
        public virtual int OdoMeterMaxValue { get; set; }
        public virtual int OdoMeterMinValue { get; set; }
        public virtual DateTime MaxDate { get; set; }
        public virtual DateTime MinDate { get; set; }
    }
}