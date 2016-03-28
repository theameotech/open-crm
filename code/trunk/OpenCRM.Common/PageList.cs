using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenCRM.Common
{
    public class PageList<T> //where T : class
    {
        public IList<T> Records = new List<T>();
        public int TotalCount = 0;
        public PageList(int totalCount, IList<T> records)
        {
            Records = records;
            TotalCount = totalCount;
        }

    }
}