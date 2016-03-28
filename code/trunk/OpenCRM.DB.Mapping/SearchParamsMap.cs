using OpenCRM.DB.DomainObjects;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenCRM.DB.Mapping
{
    public class SearchParamsMap : ClassMap<SearchParams>
    {
        public SearchParamsMap()
        {
            Table("searchparams");
            Id(x => x.Id).Not.Nullable();
            Map(x => x.ModelMaxValue);
            Map(x => x.ModelMinValue);
            Map(x => x.OdoMeterMaxValue);
            Map(x => x.OdoMeterMinValue);
            Map(x => x.MaxDate);
            Map(x => x.MinDate);
            Map(x => x.CreateTime);
            Map(x => x.LastUpdateTime);
        }
    }
}

