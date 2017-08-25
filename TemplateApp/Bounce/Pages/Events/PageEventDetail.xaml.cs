using TemplateApp.BusinessLayer.Models;
using TemplateApp.ViewModels;
using Xamarin.Forms;

namespace TemplateApp.Pages.Events
{
	public partial class PageEventDetail : ContentPage
	{
		public PageEventDetail(Event item = null)
		{
			InitializeComponent();
			BindingContext = new PageEventsDetailViewModel(item);
		}
	}
}
