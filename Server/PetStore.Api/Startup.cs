using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(PetStore.Api.Startup))]

namespace PetStore.Api
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            ConfigureAuth(app);
        }
    }
}
