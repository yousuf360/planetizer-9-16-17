using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(fyp_One.Startup))]
namespace fyp_One
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
