using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(biblioteca_2.Startup))]
namespace biblioteca_2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
