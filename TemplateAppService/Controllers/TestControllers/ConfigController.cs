using System.Web.Http;
using Microsoft.Azure.Mobile.Server.Config;
using PassTimeSportsService.Models;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Collections.Generic;
using System.Net;
using System;
using System.Net.Http;
using System.Linq;

namespace PassTimeSportsService.Controllers
{
    [MobileAppController]
    public class ConfigController : ApiController
    {
		private ConfigViewModel configuration;

		public ConfigController()
		{
			Dictionary<string, ProviderInformation> providers = new Dictionary<string, ProviderInformation>();

			AddToProviders(providers, "aad", "MOBILE_AAD_CLIENT_ID");		//Azure Active Directory
			AddToProviders(providers, "facebook", "MOBILE_FB_CLIENT_ID");		//Facebook
			AddToProviders(providers, "google", "MOBILE_GOOGLE_CLIENT_ID");		//Google +
			AddToProviders(providers, "microsoftaccount", "MOBILE_MSA_CLIENT_ID");	//Microsoft Accounts
			AddToProviders(providers, "twitter", "MOBILE_TWITTER_CLIENT_ID");	//Twitter

			configuration = new ConfigViewModel
			{
				AuthProviders = providers
			};
		}

		private void AddToProviders(Dictionary<string, ProviderInformation> providers, string provider, string envVar)
		{
			string envVal = Environment.GetEnvironmentVariable(envVar);
			if (envVal != null && envVal?.Length > 0)
			{
				providers.Add(provider, new ProviderInformation { ClientId = envVal });
			}

		}

		[HttpGet]
		public ConfigViewModel Get()
		{
			return configuration;
		}
	}

	public class ProviderInformation
	{
		public string ClientId { get; set; }
	}

	public class ConfigViewModel
	{
		public Dictionary<string, ProviderInformation> AuthProviders { get; set; }
	}
}
