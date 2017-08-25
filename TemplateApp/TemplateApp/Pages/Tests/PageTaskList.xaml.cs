using Xamarin.Forms;

namespace TemplateApp.Pages.Tests
{
	public partial class PageTaskList : ContentPage
	{
		public PageTaskList()
		{
			InitializeComponent();
			BindingContext = new ViewModels.Tests.TaskListViewModel();
		}
	}
}
