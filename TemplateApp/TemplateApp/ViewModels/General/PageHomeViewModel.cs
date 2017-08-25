using System;
using System.Diagnostics;
using System.Threading.Tasks;
using TemplateApp.Abstractions;
using TemplateApp.Pages;
using Xamarin.Forms;

namespace TemplateApp.ViewModels.General
{
	public class PageHomeViewModel : BaseViewModel
	{
		public PageHomeViewModel()
		{
			Title = "Home Page";
		}

		Command testCmd;
		Command navigateToLoginCmd;
		Command navigateToRegisterCmd;

		public Command TestCommand => testCmd ?? (testCmd = new Command(async () => await ExecuteTestCommand()));
		public Command LoginNavCommand => navigateToLoginCmd ?? (navigateToLoginCmd = new Command(async () => await ExecuteLoginNavCommand()));
		public Command RegisterNavCommand => navigateToRegisterCmd ?? (navigateToRegisterCmd = new Command(async () => await ExecuteRegisterNavCommand()));
		
		async Task ExecuteLoginNavCommand()
		{
			if (IsBusy)
				return;
			IsBusy = true;

			try
			{
				Application.Current.MainPage = new NavigationPage(new Pages.Login.PageLogin());
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"[ExecuteLoginCommand] Error = {ex.Message}");
			}
			finally
			{
				IsBusy = false;
			}
		}

		async Task ExecuteRegisterNavCommand()
		{
			if (IsBusy)
				return;
			IsBusy = true;

			try
			{
				Application.Current.MainPage = new NavigationPage(new Pages.Registration.PageRegister());
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"[ExecuteLoginCommand] Error = {ex.Message}");
			}
			finally
			{
				IsBusy = false;
			}
		}

		async Task ExecuteTestCommand()
		{
			if (IsBusy)
				return;
			IsBusy = true;

			try
			{
				Application.Current.MainPage = new NavigationPage(new Pages.Tests.PageTaskList());
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"[ExecuteLoginCommand] Error = {ex.Message}");
			}
			finally
			{
				IsBusy = false;
			}
		}
	}
}
