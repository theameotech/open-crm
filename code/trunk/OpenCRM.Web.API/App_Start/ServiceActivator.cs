using System;
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
using OpenCRM.DB.Repository;
using OpenCRM.Web.Helpers;
using OpenCRM.BizLogic.Helpers.Interfaces;
using OpenCRM.BizLogic.Helpers.Impl;
using OpenCRM.BusinessManagers.Interfaces;
using OpenCRM.BusinessManagers.Impl;
using OpenCRM.DB.Repository.Interfaces;
using OpenCRM.DB.Repository.Impl;

namespace OpenCRM.Web.API
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
                .Mappings(x => x.FluentMappings.AddFromAssemblyOf<OpenCRM.DB.Mapping.AuctionMap>())
                //.Cache(x => x.Not.UseSecondLevelCache()
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
                x.For<IBidsRepo>().Add<BidsRepo>();
                x.For<ICountryRepo>().Add<CountryRepo>();
                x.For<IVehicleRepo>().Add<VehicleRepo>();
                x.For<IMessageHelper>().Add<MessageHelper>();
                x.For<IAuctionHelper>().Add<AuctionHelper>();
                x.For<IBidsHelper>().Add<BidsHelper>();
                x.For<IBuyerHelper>().Add<BuyerHelper>();
                x.For<IDealerShipHelper>().Add<DealerShipHelper>();
                x.For<ILoginHelper>().Add<LoginHelper>();
                x.For<ILookupHelper>().Add<LookupHelper>();
                x.For<IRolesHelper>().Add<RolesHelper>();
                x.For<IUserHelper>().Add<UserHelper>();
                x.For<IVehicleHelper>().Add<VehicleHelper>();
                x.For<IMessageManager>().Add<MessageManager>();
                x.For<IAuctionManager>().Add<AuctionManager>();
                x.For<IBidsManager>().Add<BidsManager>();
                x.For<IBuyerManager>().Add<BuyerManager>();
                x.For<IDealerShipManager>().Add<DealerShipManager>();
                x.For<ILoginManager>().Add<LoginManager>();
                x.For<ILookupManager>().Add<LookupManager>();
                x.For<IRolesManager>().Add<RolesManager>();
                x.For<IUserManager>().Add<UserManager>();
                x.For<IVehicleManager>().Add<VehicleManager>();
                x.For<ICompanyHelper>().Add<CompanyHelper>();
                x.For<ICompanyManager>().Add<CompanyManager>();
                x.For<IInboxHelper>().Add<InboxHelper>();
                x.For<IInboxManager>().Add<InboxManager>();
                x.For<IInboxRepo>().Add<InboxRepo>();
                x.For<IDoListHelper>().Add<DoListHelper>();
                x.For<IDoListManager>().Add<DoListManager>();
                x.For<IDoListRepo>().Add<DoListRepo>();
            });
        }
    }
}