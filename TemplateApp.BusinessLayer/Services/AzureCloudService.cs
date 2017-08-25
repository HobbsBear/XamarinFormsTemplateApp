using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json.Linq;
using TemplateApp.Abstractions;
using TemplateApp.Helpers;
using TemplateApp.BusinessLayer.Models;
using TemplateApp.BusinessLayer.Services;

namespace TemplateApp.BusinessLayer.Services
{
	public class AzureCloudService : ICloudService
	{
		MobileServiceClient client;

		public AzureCloudService()
		{
			client = new MobileServiceClient(Locations.AppServiceUrl, new AuthenticationDelegatingHandler());

			if (Locations.AlternateLoginHost != null)
				client.AlternateLoginHost = new Uri(Locations.AlternateLoginHost);
		}

		public ICloudTable<T> GetTable<T>() where T : TableData => new AzureCloudTable<T>(client);

		public async Task<MobileServiceUser> LoginAsync()
		{
			return await UserService.LoginAsync();
		}

		public async Task LogoutAsync()
		{
			await UserService.LogoutAsync();
		}

		public Task LoginAsync(User user)
		{
			return client.LoginAsync("custom", JObject.FromObject(user));
		}

		public async Task RegisterUserAsync(User user)
		{
			await UserService.RegisterUserAsync(user);
		}

		public async Task<User> getUserByID(string userID)
		{
			return await UserService.getUserByID(userID);
		}

		public async Task<AppServiceIdentity> GetIdentityAsync()
		{
			return await UserService.GetIdentityAsync();
		}		
	}
}
