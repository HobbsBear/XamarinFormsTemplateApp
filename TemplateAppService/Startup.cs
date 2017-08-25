using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(TemplateAppService.Startup))]

namespace TemplateAppService
{
	public partial class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			ConfigureMobileApp(app);
		}
	}
}