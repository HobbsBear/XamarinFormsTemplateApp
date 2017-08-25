using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace TemplateApp.Pages.Registration
{
	public partial class PageRegisterPart2 : ContentPage
	{
		public PageRegisterPart2()
		{
			InitializeComponent();

            //tbxUsername.Completed += (s, e) => tbxPassword.Focus();
		}

        private async void btnNext_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Registration.PageRegisterPart3());
        }
    }
}
