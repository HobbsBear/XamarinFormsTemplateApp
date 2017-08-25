using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateApp.BusinessLayer.Models;

using Xamarin.Forms;

namespace TemplateApp.Pages.General
{
	public partial class PageMain : ContentPage
	{
		private User user;

		public PageMain(User user)
		{
			InitializeComponent();
			BindingContext = new ViewModels.General.PageMainViewModel(user);
		}

		private void listView_ItemTapped(object sender, ItemTappedEventArgs e)
		{
		}

		private void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
		}

		private void Join_Clicked(object sender, EventArgs e)
		{
		}
	}
}
