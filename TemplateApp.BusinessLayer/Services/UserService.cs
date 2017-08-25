using System;
using System.Diagnostics;
using Microsoft.WindowsAzure.MobileServices;
using TemplateApp.Abstractions;
using Xamarin.Forms;
using TemplateApp.BusinessLayer.Models;
using System.Collections.Generic;
using TemplateApp.Helpers;
using System.Threading.Tasks;
using System.Net.Http;

namespace TemplateApp.BusinessLayer.Services
{
	public class UserService
	{

		static MobileServiceClient client;
		static List<AppServiceIdentity> identities = null;

		public UserService()
		{
			client = new MobileServiceClient(Locations.AppServiceUrl, new AuthenticationDelegatingHandler());

			if (Locations.AlternateLoginHost != null)
				client.AlternateLoginHost = new Uri(Locations.AlternateLoginHost);

		}

		public static async Task<User> getCurrentLoggedInUser()
		{
			return await getUserByID(client.CurrentUser.UserId);
		}

		public static async Task<MobileServiceUser> LoginAsync()
		{
			var loginProvider = DependencyService.Get<ILoginProvider>();

			client.CurrentUser = loginProvider.RetrieveTokenFromSecureStore();
			if (client.CurrentUser != null)
			{
				// User has previously been authenticated - try to Refresh the token
				try
				{
					var refreshed = await client.RefreshUserAsync();
					if (refreshed != null)
					{
						loginProvider.StoreTokenInSecureStore(refreshed);
						return refreshed;
					}
				}
				catch (Exception refreshException)
				{
					Debug.WriteLine($"Could not refresh token: {refreshException.Message}");
				}
			}

			if (client.CurrentUser != null && !ToolsService.IsTokenExpired(client.CurrentUser.MobileServiceAuthenticationToken))
			{
				// User has previously been authenticated, no refresh is required
				return client.CurrentUser;
			}

			// We need to ask for credentials at this point
			await loginProvider.LoginAsync(client);
			if (client.CurrentUser != null)
			{
				// We were able to successfully log in
				loginProvider.StoreTokenInSecureStore(client.CurrentUser);
			}
			return client.CurrentUser;
		}

		public static async Task<AppServiceIdentity> GetIdentityAsync()
		{
			if (client.CurrentUser == null || client.CurrentUser?.MobileServiceAuthenticationToken == null)
			{
				throw new InvalidOperationException("Not Authenticated");
			}

			if (identities == null)
			{
				identities = await client.InvokeApiAsync<List<AppServiceIdentity>>("/.auth/me");
			}

			if (identities.Count > 0)
				return identities[0];
			return null;
		}

		public async static Task<User> getUserByID(string userID)
		{
			Dictionary<string, string> parameters = new Dictionary<string, string>();

			parameters.Add("userID", userID.ToString());

			var result = await client.InvokeApiAsync("User", System.Net.Http.HttpMethod.Get, parameters);

			if (result == null || result.ToString() == "null")
			{
				return null;
			}

			User u = new User()
			{
				UserId = result.Value<int>("userId"),
				Username = result.Value<string>("username"),
				FirstName = result.Value<string>("firstName"),
				LastName = result.Value<string>("lastName"),
				EmailAddress = result.Value<string>("emailAddress")
			};

			return u;
		}

		public static async Task RegisterUserAsync(User user)
		{
			Dictionary<string, string> parameters = new Dictionary<string, string>();
			parameters.Add("first", user.FirstName);
			parameters.Add("last", user.LastName);
			parameters.Add("pass", user.Password);
			parameters.Add("email", user.EmailAddress);
			parameters.Add("username", user.Username);
			var result = await client.InvokeApiAsync("Registration", System.Net.Http.HttpMethod.Get, parameters);
			return;
		}

		public static async Task LogoutAsync()
		{
			if (client.CurrentUser == null || client.CurrentUser.MobileServiceAuthenticationToken == null)
				return;

			// Log out of the identity provider (if required)

			// Invalidate the token on the mobile backend
			var authUri = new Uri($"{client.MobileAppUri}/.auth/logout");
			using (var httpClient = new HttpClient())
			{
				httpClient.DefaultRequestHeaders.Add("X-ZUMO-AUTH", client.CurrentUser.MobileServiceAuthenticationToken);
				await httpClient.GetAsync(authUri);
			}

			// Remove the token from the cache
			DependencyService.Get<ILoginProvider>().RemoveTokenFromSecureStore();

			// Remove the token from the MobileServiceClient
			await client.LogoutAsync();
		}
	}
}
