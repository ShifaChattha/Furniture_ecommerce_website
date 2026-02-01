using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Project.net7Oct.Startup))]
namespace Project.net7Oct
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
