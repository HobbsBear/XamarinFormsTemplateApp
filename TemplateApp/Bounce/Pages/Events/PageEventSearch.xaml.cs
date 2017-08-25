using Xamarin.Forms;

namespace TemplateApp.Pages.Events
{
	public partial class PageEventSearch : ContentPage
	{
		public PageEventSearch()
		{
			InitializeComponent();
			BindingContext = new ViewModels.Events.PageEventSearchViewModel();
		}
	}
}
