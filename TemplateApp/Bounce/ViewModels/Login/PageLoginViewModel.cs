using System;
using System.Diagnostics;
using System.Threading.Tasks;
using TemplateApp.Abstractions;
using TemplateApp.Pages;
using Xamarin.Forms;
using TemplateApp.Helpers;
using TemplateApp.BusinessLayer.Models;

namespace TemplateApp.ViewModels.Login
{
	public class PageLoginViewModel : BaseViewModel
	{
		private ICloudService cloudService;

		public PageLoginViewModel()
		{
			cloudService = ServiceLocator.Instance.Resolve<ICloudService>();
			Title = "Task List";
			User = new User { Username = "", Password = "" };
			LoginCommand = new Command(async () => await ExecuteLoginCommand());

			checkCurrentLogin();
		}

		private async Task checkCurrentLogin()
		{
			User u = await cloudService.getUserByID(1);
			return;
		}

		public Command LoginCommand { get; }
		public User User { get; set; }
	
		async Task ExecuteLoginCommand()
		{
			if (IsBusy)
				return;
			IsBusy = true;

			try
			{
				var cloudService = ServiceLocator.Instance.Resolve<ICloudService>();
				await cloudService.LoginAsync(User);
				//10001110101 This is 100% absolutely wrong. Passing the unresolved user back is just irresponsible.
				Application.Current.MainPage = new NavigationPage(new Pages.General.PageMain(User));
			}
			catch (Exception ex)
			{
				await App.Current.MainPage.DisplayAlert("Could not log in User. Error: " + ex.Message, "Login Failure", "Cancel");
				Debug.WriteLine($"[ExecuteLoginCommnad] Error = {ex.Message}");
			}
			finally
			{
				IsBusy = false;
			}
		}
	}
}
