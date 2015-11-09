namespace PetStore.Api
{
    using System.Web;
    using System.Web.Http;

    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            DatabaseConfig.Initialize();
            AutoMapperConfig.RegisterMappings();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
