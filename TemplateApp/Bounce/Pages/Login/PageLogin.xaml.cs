using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace TemplateApp.Pages.Login
{
	public partial class PageLogin : ContentPage
	{
		public PageLogin()
		{
			InitializeComponent();
			BindingContext = new ViewModels.Login.PageLoginViewModel();
		}

		async void lblPasswordReset_Clicked(object sender, System.EventArgs e)
		{
			await DisplayAlert("Not Implemented", "Hello User, we apologize for the inconvenience, but that doesn't work just yet!", "Cancel");
		}

		async void btnBack_Click(object sender, System.EventArgs e)
		{
			await Navigation.PopAsync();
		}
	}
}
