using TemplateApp.BusinessLayer.Services;
using TemplateApp.Helpers;
using TemplateApp.Abstractions;
using Xamarin.Forms;

namespace TemplateApp
{
	public class App : Application
	{
		public static ICloudService CloudService { get; set; }

		public App()
		{
			ServiceLocator.Instance.Add<ICloudService, AzureCloudService>();
			MainPage = new NavigationPage(new Pages.General.PageHome());
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
