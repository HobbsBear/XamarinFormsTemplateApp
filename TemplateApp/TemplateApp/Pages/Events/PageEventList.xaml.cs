using Xamarin.Forms;

namespace TemplateApp.Pages.Events
{
	public partial class PageEventList : ContentPage
	{
		public PageEventList()
		{
			InitializeComponent();
			BindingContext = new ViewModels.Events.PageEventsListViewModel();
		}
	}
}
