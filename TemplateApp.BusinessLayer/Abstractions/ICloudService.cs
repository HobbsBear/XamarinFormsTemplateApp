using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using TemplateApp.BusinessLayer.Models;

namespace TemplateApp.Abstractions
{
	public interface ICloudService
	{
		//basically used for initializing the connection and getting a table definition
		ICloudTable<T> GetTable<T>() where T : TableData;

		Task<MobileServiceUser> LoginAsync();

		Task LogoutAsync();

		Task LoginAsync(User user);

		Task<AppServiceIdentity> GetIdentityAsync();

		Task RegisterUserAsync(User user);

		Task<User> getUserByID(string userID);
	}
}