﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using StructureMap;
using NHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using AC.Repository;
using AC.helpers;

namespace OpenCRM.Web.App_Start
{
    public class ServiceActivator : IHttpControllerActivator
    {
        public ServiceActivator(HttpConfiguration configuration) { }

        public IHttpController Create(HttpRequestMessage request
            , HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            var controller = ObjectFactory.Container.GetInstance(controllerType) as IHttpController;
            return controller;
        }


    }
    public static class ObjectFactory
    {
        public static ISessionFactory SessionFactory
        {
            get;
            private set;
        }

        private static ISession GetSession(IContext context)
        {
            ISessionFactory factory = context.GetInstance<ISessionFactory>();
            ISession ssession = null;
            if (HttpContext.Current != null)
            {
                if (NHibernate.Context.CurrentSessionContext.HasBind(factory))
                    return factory.GetCurrentSession();

                ssession = factory.OpenSession();
                NHibernate.Context.CurrentSessionContext.Bind(ssession);
            }
            else
            {
                ssession = factory.OpenSession();
            }
            return ssession;

        }

        /// <summary>
        /// Creates the session factory.
        /// </summary>
        /// <returns></returns>
        private static ISessionFactory CreateSessionFactory()
        {
            return SessionFactory = Fluently.Configure()
                .Database(FluentNHibernate.Cfg.Db.MySQLConfiguration.Standard
                .ConnectionString(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                .ExposeConfiguration(c => c.SetProperty("current_session_context_class", "web"))
                .Mappings(x => x.FluentMappings.AddFromAssemblyOf<AC.Mapping.AuctionMap>())
                //.Cache(x => x.Not.UseSecondLevelCache())
                .Cache(x => x.Not.UseQueryCache())
                .BuildSessionFactory();
        }

        private static readonly Lazy<StructureMap.Container> _containerBuilder =
                new Lazy<StructureMap.Container>(DefaultContainer, LazyThreadSafetyMode.ExecutionAndPublication);

        public static StructureMap.IContainer Container
        {
            get { return _containerBuilder.Value; }
        }

        private static StructureMap.Container DefaultContainer()
        {
            return new StructureMap.Container(x =>
            {
                x.For<ISessionFactory>().Singleton().Use(CreateSessionFactory());
                x.For<ISession>().Singleton().Use(context => GetSession(context));
                x.For(typeof(IRepository<>)).Use(typeof(Repository<>));
                x.For<IUserRepo>().Use<UserRepo>();
                x.For<IUserRoleRepo>().Use<UserRoleRepo>();
                x.For<IRoleRepo>().Use<RoleRepo>();
                x.For<IBuyerRepo>().Add<BuyerRepo>();
                x.For<IAuctionRepo>().Add<AuctionRepo>();
                x.For<ISearchParamsRepo>().Add<SearchParamsRepo>();
                x.For<IDealerShipRepo>().Add<DealerShipRepo>();
                x.For<ICompanyRepo>().Add<CompanyRepo>();
                x.For<IMessageRepo>().Add<MessageRepo>();
                x.For<IMessageHelper>().Add<MessageHelper>();
            });
        }
    }
}