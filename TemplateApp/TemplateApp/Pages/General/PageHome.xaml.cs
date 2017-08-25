using TemplateApp.ViewModels;

using Xamarin.Forms;

namespace TemplateApp.Pages.General
{
	public partial class PageHome : ContentPage
	{
		public PageHome()
		{
			InitializeComponent();
			BindingContext = new ViewModels.General.PageHomeViewModel();
		}
	}
}
