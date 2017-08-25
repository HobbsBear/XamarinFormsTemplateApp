using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(PassTimeService.Startup))]

namespace PassTimeService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}