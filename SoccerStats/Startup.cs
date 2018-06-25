using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SoccerStats.Startup))]
namespace SoccerStats
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
