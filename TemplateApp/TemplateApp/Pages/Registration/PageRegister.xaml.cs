using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace TemplateApp.Pages.Registration
{
	public partial class PageRegister : ContentPage
	{
		public PageRegister()
		{
			InitializeComponent();
			BindingContext = new ViewModels.Registration.PageRegistrationViewModel();
		}
	}
}
