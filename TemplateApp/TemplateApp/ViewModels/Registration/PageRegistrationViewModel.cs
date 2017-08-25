using System;
using System.Diagnostics;
using System.Threading.Tasks;
using TemplateApp.Abstractions;
using TemplateApp.Pages;
using Xamarin.Forms;
using TemplateApp.Helpers;
using TemplateApp.BusinessLayer.Models;

namespace TemplateApp.ViewModels.Registration
{
	public class PageRegistrationViewModel : BaseViewModel
	{
		private ICloudService cloudService;
		public PageRegistrationViewModel()
		{

			cloudService = ServiceLocator.Instance.Resolve<ICloudService>();
			Title = "Register";
			passwordConfirm = "";
			User = new User { Username = "", Password = "", FirstName = "", LastName = "", EmailAddress = "" };
			RegisterCommand = new Command(async () => await ExecuteRegisterCommand());
		}

		public Command RegisterCommand { get; }
		public User User { get; set; }
		public string passwordConfirm { get; set; }

		async Task ExecuteRegisterCommand()
		{
			if (IsBusy)
				return;
			IsBusy = true;

			string RegisterFailureMessage = "";

			if (string.IsNullOrEmpty(User.Username))
			{
				RegisterFailureMessage = "No Username entered. Please try again.";
			}
			else if (string.IsNullOrEmpty(User.FirstName) || string.IsNullOrEmpty(User.LastName))
			{
				RegisterFailureMessage = "First or Last name not set. Please try again.";
			}
			else if (string.IsNullOrEmpty(User.Password) || string.IsNullOrEmpty(passwordConfirm) || (User.Password != passwordConfirm))
			{
				RegisterFailureMessage = "Password field was empty or did not match. Please try again.";
			}
			else if (string.IsNullOrEmpty(User.EmailAddress))
			{
				RegisterFailureMessage = "Email field was empty. Please try again.";
			}
            else if (!ValidateEmail(User.EmailAddress))
            {
                RegisterFailureMessage = "Email is invalid. Please try again.";
            }

			if (RegisterFailureMessage != "")
			{
				await App.Current.MainPage.DisplayAlert("Could not register user. " + RegisterFailureMessage , "Register Failure", "Okay");
				passwordConfirm = "";
				User.Password = "";
				IsBusy = false;
				return;
			}

			try
			{
				await cloudService.RegisterUserAsync(User);
				//10001110101 This is 100% absolutely wrong. Passing the unresolved user back is just irresponsible.
				await cloudService.LoginAsync(User);
				Application.Current.MainPage = new NavigationPage(new Pages.General.PageMain(User));
			}
			catch (Exception ex)
			{
				await Application.Current.MainPage.DisplayAlert("Register User Failed", ex.Message, "OK");
			}
			finally
			{
				IsBusy = false;
			}
		}

        private bool ValidateEmail(string emailAddress)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            System.Text.RegularExpressions.Match match = regex.Match(emailAddress);
            return match.Success;
        }
    }
}
