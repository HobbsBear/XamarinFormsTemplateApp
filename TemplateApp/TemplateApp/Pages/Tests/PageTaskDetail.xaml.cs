using TemplateApp.BusinessLayer.Models;
using TemplateApp.ViewModels;
using Xamarin.Forms;

namespace TemplateApp.Pages.Tests
{
	public partial class PageTaskDetail : ContentPage
	{
		public PageTaskDetail(ToDoItem item = null)
		{
			InitializeComponent();
			BindingContext = new TaskDetailViewModel(item);
		}
	}
}
