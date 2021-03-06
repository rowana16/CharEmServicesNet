[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(CharEmServicesNet.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(CharEmServicesNet.App_Start.NinjectWebCommon), "Stop")]

namespace CharEmServicesNet.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Models;
    using static Models.IRepository;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                kernel.Bind<IGenericRepository<Service>>().To<EFServiceRepository>();
                kernel.Bind<IGenericRepository<ServiceProvider>>().To<EFProviderRepository>();
                kernel.Bind<IGenericRepository<ServiceRecipient>>().To<EFRecipientRepository>();
                kernel.Bind<IGenericRepository<ServiceType>>().To<EFServiceTypeRepository>();
                kernel.Bind<IUserRepository>().To<EFUserRepository>();
                kernel.Bind<IGenericRepository<Location>>().To<EFLocationRepository>();
                kernel.Bind<IGenericRepository<City>>().To<EFCityRepository>();
                kernel.Bind<IGenericRepository<County>>().To<EFCountyRepository>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
        }        
    }
}
