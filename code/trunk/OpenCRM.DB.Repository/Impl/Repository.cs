using OpenCRM.DB.DomainObjects;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using NHibernate.Linq;
using FluentNHibernate.Cfg;
using System.Configuration;
namespace OpenCRM.DB.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseDomainObject
    {
        private readonly ISession _session;
        public Repository(ISession session)
        {
            _session = session;
        }
        public void Add(T entity)
        {
            BaseDomainObject baseObject = (BaseDomainObject)entity;
            baseObject.CreateTime = DateTime.Now;
            baseObject.LastUpdateTime = DateTime.Now;
            _session.Save(entity);
        }

        public void Update(T entity)
        {
            BaseDomainObject baseObject = (BaseDomainObject)entity;
            baseObject.LastUpdateTime = DateTime.Now;
            _session.SaveOrUpdate(entity);
        }

        public IList<T> FetchAll()
        {
            return _session.Query<T>().ToList();
        }


        public IList<T> GetAll()
        {
            return _session.Query<T>().ToList();
        }
        public T Get(Expression<Func<T, bool>> expression)
        {
            return _session.Query<T>().FirstOrDefault(expression);
        }

        public bool IsExist(Expression<Func<T, bool>> expression)
        {
            var qureyResults = _session.Query<T>().Where(expression);
            return (qureyResults != null && qureyResults.Count() > 0);
        }

        public IList<T> FetchAll(Expression<Func<T, bool>> expression)
        {
            return _session.Query<T>().Where(expression).ToList();
        }

        public void Delete(T entity)
        {
            _session.Delete(entity);
        }
    }
}