using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(PetStore.Api.Startup))]

namespace PetStore.Api
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
