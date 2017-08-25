using Xamarin.Forms;

namespace TemplateApp.Pages.Events
{
	public partial class PageEventSearchParameters : ContentPage
	{
		public PageEventSearchParameters()
		{
			InitializeComponent();
			BindingContext = new ViewModels.Events.PageEventSearchParametersViewModel();
		}
	}
}
