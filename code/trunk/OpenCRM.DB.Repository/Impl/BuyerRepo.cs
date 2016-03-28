using OpenCRM.DB.DomainObjects;
using NHibernate;
using NHibernate.Transform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Linq;
using OpenCRM.Common.DTO;
using OpenCRM.Common;
namespace OpenCRM.DB.Repository
{
    public class BuyerRepo : Repository<Buyers>, IBuyerRepo
    {
        private readonly ISession _session;
        public BuyerRepo(ISession session)
            : base(session)
        {
            _session = session;
        }


        public IList<VehicleInfo> GetVehicleInfoByBuyer(int buyerId, string searchText,
            int modelMin, int modelMax, int odometeMin, int odomoterMax, DateTime minDate, DateTime maxDate)
        {
            maxDate = maxDate.AddDays(1).AddSeconds(-1);
            string minimumDate = String.Format("{0:s}", minDate);
            string maximumDate = String.Format("{0:s}", maxDate);
            string query = string.Format(@"Select byr.Name as BuyerName, byr.BuyerAddress,
                                            byr.ContactFirstName,
                                            byr.BuyerPhone,  
                                            v.Name, v.Odometer, v.Model, v.Vin, b.Amount, b.Type, v.Id, a.AuctionDate from bids b
                                            inner join vehicle v on b.vehicleid = v.id                                            
                                            inner join auction a on a.id = b.auctionid
                                            inner join buyers byr on byr.id = b.buyerid
                                            where 
                                                  CAST(v.model AS UNSIGNED) between {0} and {1}
                                                  and CAST(v.odometer AS UNSIGNED) between {2} and {3}
                                                  and (v.model like '%{4}%' or v.odometer like '%{4}%' or v.vin like '%{4}%' or v.name like '%{4}%')  
                                                  and UNIX_TIMESTAMP(v.createtime)
												                BETWEEN UNIX_TIMESTAMP('{6}') AND UNIX_TIMESTAMP('{7}'
                                                  )                                             
                                            and b.buyerid = {5}", modelMin, modelMax, odometeMin, odomoterMax, searchText, buyerId, minimumDate, maximumDate);

            var sqlQuery = _session.CreateSQLQuery(query);

            return sqlQuery
                 .SetResultTransformer(Transformers.AliasToBean(typeof(VehicleInfo)))
                 .List<VehicleInfo>();
        }
        public IList<MinMaxValues> GetMinMaxValues()
        {
            string query = string.Format(@"SELECT min(odometer) as MinOdometer, max(odometer) as MaxOdometer,min(model) as MinModel,max(model) as MaxModel,min(createtime) as MinCreateDate,Max(createtime) as MaxCreateDate FROM vehicle;");

            var sqlQuery = _session.CreateSQLQuery(query);

            return sqlQuery
                 .SetResultTransformer(Transformers.AliasToBean(typeof(MinMaxValues)))
                 .List<MinMaxValues>();
        }
        public PageList<VehicleCore> GetVehicleInfoByAuction(int auctionId, int page, int pageSize)
        {
            string query = string.Format(@"Select distinct v.Name, v.Odometer, v.Model, v.Vin, v.Id from bids b
                                inner join vehicle v on b.vehicleid = v.id                                 
                                where b.auctionid = {0}", auctionId);

            var sqlQuery = _session.CreateSQLQuery(query);

            int totalRecords = sqlQuery
             .SetResultTransformer(Transformers.AliasToBean(typeof(VehicleCore)))
             .List<VehicleCore>().Count();

            return ToPageList<VehicleCore>(page, pageSize, sqlQuery, totalRecords);
        }

        private static PageList<T> ToPageList<T>(int page, int pageSize, ISQLQuery sqlQuery, int totalRecords)
        {
            IList<T> records = new List<T>();

            records = sqlQuery
                    .SetResultTransformer(Transformers.AliasToBean(typeof(T)))
                    .List<T>()
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize).ToList();

            var vehiclesList = new PageList<T>(totalRecords, records);
            return vehiclesList;
        }

        public PageList<VehicleInfo> GetBidsInfoByVehicle(int vehicleId, int page, int pageSize)
        {

            string query = string.Format(@"Select byr.Name as BuyerName, byr.BuyerAddress,
                                byr.ContactFirstName, byr.ContactLastName,
                                byr.BuyerPhone,byr.BuyerEmail,
                                v.Name, v.Odometer, v.Model, v.Vin, b.Amount, b.Type, v.Id, a.AuctionDate from bids b
                                inner join vehicle v on b.vehicleid = v.id 
                                inner join auction a on a.id = b.auctionid
                                inner join buyers byr on byr.id = b.buyerid
                                where v.id = {0}", vehicleId);

            var sqlQuery = _session.CreateSQLQuery(query);

            int totalRecords = sqlQuery
           .SetResultTransformer(Transformers.AliasToBean(typeof(VehicleInfo)))
           .List<VehicleInfo>().Count();

            return ToPageList<VehicleInfo>(page, pageSize, sqlQuery, totalRecords);
        }

        public IList<Buyers> GetBuyerRecords(string searchText,
            int modelMin, int modelMax, int odometeMin, int odomoterMax, DateTime maxDate, DateTime minDate)
        {
            maxDate = maxDate.AddDays(1).AddSeconds(-1);
            string minimumDate = String.Format("{0:s}", minDate);
            string maximumDate = String.Format("{0:s}", maxDate);
            string query = string.Format(@"SELECT distinct Id, Name,ContactFirstName,ContactLastName, BuyerPhone, BuyerAddress ,BuyerEmail,
                                            CreateTime FROM buyers
                                            WHERE id IN (
                                               SELECT t.buyerid FROM (
                                                  SELECT act.buyerid 
                                                  FROM bids act 
                                                  INNER JOIN buyers u 
                                                  ON act.buyerid=u.id                                                   
                                                  INNER JOIN auction a 
                                                  ON a.id=act.auctionid
                                                  inner join vehicle v on 
                                                  v.id = act.vehicleid                                                  
                                                  where 
                                                  CAST(v.model AS UNSIGNED) between {0} and {1}
                                                  and CAST(v.odometer AS UNSIGNED) between {2} and {3}
                                                      and (v.model like '%{4}%' or v.odometer like '%{4}%' or v.vin like '%{4}%' or v.name like '%{4}%')    
                                                      and UNIX_TIMESTAMP(v.createtime)
												                BETWEEN UNIX_TIMESTAMP('{5}') AND UNIX_TIMESTAMP('{6}')                                              
                                                      GROUP BY act.buyerid 
                                                  ORDER BY COUNT(act.buyerid) 
                                                  DESC                                   
                                                ) AS t                                                
                                            )", modelMin, modelMax, odometeMin, odomoterMax, searchText, minimumDate, maximumDate);

            var sqlQuery = _session.CreateSQLQuery(query);

            return sqlQuery
                .SetResultTransformer(Transformers.AliasToBean(typeof(Buyers)))
                .List<Buyers>();
        }

        public PageList<Buyers> Search(int page, int pageSize, string searchText,
            int modelMin, int modelMax, int odometeMin, int odomoterMax, DateTime maxDate, DateTime minDate)
        {
            maxDate = maxDate.AddDays(1).AddSeconds(-1);
            string minimumDate = String.Format("{0:s}", minDate);
            string maximumDate = String.Format("{0:s}", maxDate);

            string query = string.Format(@"SELECT distinct Id, Name,ContactFirstName,ContactLastName, BuyerPhone, BuyerAddress ,BuyerEmail,
                                            CreateTime FROM buyers
                                            WHERE id IN (
                                               SELECT t.buyerid FROM (
                                                  SELECT act.buyerid 
                                                  FROM bids act 
                                                  INNER JOIN buyers u 
                                                  ON act.buyerid=u.id                                                   
                                                  INNER JOIN auction a 
                                                  ON a.id=act.auctionid
                                                  inner join vehicle v on 
                                                  v.id = act.vehicleid                                                  
                                                  where 
                                                  CAST(v.model AS UNSIGNED) between {0} and {1}
                                                  and CAST(v.odometer AS UNSIGNED) between {2} and {3}
                                                      and (v.model like '%{4}%' or v.odometer like '%{4}%' or v.vin like '%{4}%' or v.name like '%{4}%') 
                                                 and UNIX_TIMESTAMP(v.createtime)
												                BETWEEN UNIX_TIMESTAMP('{5}') AND UNIX_TIMESTAMP('{6}')
                                                      GROUP BY act.buyerid 
                                                  ORDER BY COUNT(act.buyerid) 
                                                  DESC                                   
                                                ) AS t                                                
                                            )", modelMin, modelMax, odometeMin, odomoterMax, searchText, minimumDate, maximumDate);

            var sqlQuery = _session.CreateSQLQuery(query);

            int totalRecords = sqlQuery
                .SetResultTransformer(Transformers.AliasToBean(typeof(Buyers)))
                .List<Buyers>().Count();

            return ToPageList<Buyers>(page, pageSize, sqlQuery, totalRecords);
        }

        public PageList<Buyers> FetchActiveBuyerWithInTimeInterval(int timeInterval, string intervalType,
            int page, int pageSize)
        {
            string query = string.Format(@"SELECT distinct Id, Name,ContactFirstName,ContactLastName, BuyerPhone, BuyerAddress,BuyerEmail  FROM buyers
                                            WHERE id IN (
                                               SELECT t.buyerid FROM (
                                                  SELECT act.buyerid 
                                                  FROM bids act 
                                                  INNER JOIN buyers u 
                                                  ON act.buyerid=u.id  
                                                  INNER JOIN auction a 
                                                  ON a.id=act.auctionid
                                                  where DATE(u.createtime)
												                BETWEEN DATE(DATE_ADD(now(), INTERVAL -1 Year)) AND  DATE(now())                             
                                                  GROUP BY act.buyerid 
                                                  ORDER BY COUNT(act.buyerid) 
                                                  DESC                                   
                                                ) AS t                                                
                                            );", timeInterval, intervalType);

            var sqlQuery = _session.CreateSQLQuery(query);

            int totalRecords = sqlQuery
                .SetResultTransformer(Transformers.AliasToBean(typeof(Buyers)))
                .List<Buyers>().Count();

            return ToPageList<Buyers>(page, pageSize, sqlQuery, totalRecords);
        }
    }
}