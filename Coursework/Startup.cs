using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Coursework.Startup))]
namespace Coursework
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
