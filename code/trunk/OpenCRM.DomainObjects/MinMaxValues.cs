using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenCRM.DB.DomainObjects
{
    public class MinMaxValues
    {
        public long MinOdometer { get; set; }
        public long MaxOdometer { get; set; }
        
        public string MinModel { get; set; }
        public string MaxModel { get; set; }

        public DateTime MinCreateDate { get; set; }
        public DateTime MaxCreateDate { get; set; }
    }
}