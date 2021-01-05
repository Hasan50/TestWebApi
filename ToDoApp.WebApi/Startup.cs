using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ToDoApp.WebApi.Startup))]
namespace ToDoApp.WebApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
