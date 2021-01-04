using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CateringSystem.WebApi.Startup))]
namespace CateringSystem.WebApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
