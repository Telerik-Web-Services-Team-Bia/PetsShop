[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(PetStore.Api.App_Start.NinjectConfig), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(PetStore.Api.App_Start.NinjectConfig), "Stop")]

namespace PetStore.Api.App_Start
{
    using System;
    using System.Data.Entity;
    using System.Web;

    using Data;
    using Data.Repositories;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Services.Data;
    using Services.Data.Contracts;

    public static class NinjectConfig 
    {
        private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

        // should be used only in integration testing scenarios
        public static Action<IKernel> RebindAction { get; set; }
        
        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            Bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            Bootstrapper.ShutDown();
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
            kernel.Bind(typeof(IRepository<>)).To(typeof(EfGenericRepository<>));
            kernel.Bind<DbContext>().To<PetStoreDbContext>().InRequestScope();
            kernel.Bind<ICategoriesService>().To<CategoriesService>().InRequestScope();
            kernel.Bind<IPetsService>().To<PetsService>().InRequestScope();
            kernel.Bind<IRatingsService>().To<RatingsService>().InRequestScope();
        }        
    }
}
