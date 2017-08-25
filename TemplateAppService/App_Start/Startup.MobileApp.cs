using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Web.Http;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Authentication;
using Microsoft.Azure.Mobile.Server.Config;
using TemplateAppService.DataObjects;
using TemplateAppService.Models;
using Owin;
using System.Data.Entity.Migrations;

namespace TemplateAppService
{
	public partial class Startup
	{
		public static void ConfigureMobileApp(IAppBuilder app)
		{
			HttpConfiguration config = new HttpConfiguration();
			config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

			new MobileAppConfiguration()
			    .AddTablesWithEntityFramework()
			    .MapApiControllers()
			    .ApplyTo(config);

			// Map routes by attribute
			config.MapHttpAttributeRoutes();

			// Use Entity Framework Code First to create database tables based on your DbContext
			Database.SetInitializer(new MobileServiceInitializer());

			MobileAppSettingsDictionary settings = config.GetMobileAppSettingsProvider().GetMobileAppSettings();

			if (string.IsNullOrEmpty(settings.HostName))
			{
				app.UseAppServiceAuthentication(new AppServiceAuthenticationOptions
				{
					SigningKey = ConfigurationManager.AppSettings["SigningKey"],
					ValidAudiences = new[] { ConfigurationManager.AppSettings["ValidAudience"] },
					ValidIssuers = new[] { ConfigurationManager.AppSettings["ValidIssuer"] },
					TokenHandler = config.GetAppServiceTokenHandler()
				});
			}

			app.UseWebApi(config);
		}
	}

	public class MobileServiceInitializer : CreateDatabaseIfNotExists<MobileServiceContext>
	{
		protected override void Seed(MobileServiceContext context)
		{
			//HEY!!! This isn't actually used. You want the Seed in Configuration.cs under the Migrations folder.
		}
	}
}

