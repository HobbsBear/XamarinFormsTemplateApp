using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace TemplateApp.Pages.Registration
{
    public partial class PageRegisterPart3 : ContentPage
    {
        public PageRegisterPart3()
        {
            InitializeComponent();

            pickGender.Items.Add("Male");
            pickGender.Items.Add("Female");

           
        }

        private async void btnCreateAccount_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new General.PageMain(null));
        }

        private void pickGender_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
