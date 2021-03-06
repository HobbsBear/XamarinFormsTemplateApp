﻿using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TemplateApp.Abstractions;
using TemplateApp.Helpers;
using TemplateApp.BusinessLayer.Models;
using Xamarin.Forms;

namespace TemplateApp.ViewModels.Tests
{
	public class TaskListViewModel : BaseViewModel
	{
		ICloudService cloudService;
		public ICloudTable<ToDoItem> Table { get; set; }
		public Command RefreshCommand { get; }
		public Command AddNewItemCommand { get; }
		public Command LogoutCommand { get; }

		public TaskListViewModel()
		{
			Debug.WriteLine("In TaskListViewMOdel");
			cloudService = ServiceLocator.Instance.Resolve<ICloudService>();
			Table = cloudService.GetTable<ToDoItem>();

			Title = "Task List";
			items.CollectionChanged += this.OnCollectionChanged;

			RefreshCommand = new Command(async () => await ExecuteRefreshCommand());
			AddNewItemCommand = new Command(async () => await ExecuteAddNewItemCommand());
			LogoutCommand = new Command(async () => await ExecuteLogoutCommand());

			// Execute the refresh command
			RefreshCommand.Execute(null);
		}

		void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			Debug.WriteLine("[TaskList] OnCollectionChanged: Items have changed");
		}

		ObservableRangeCollection<ToDoItem> items = new ObservableRangeCollection<ToDoItem>();
		public ObservableRangeCollection<ToDoItem> Items
		{
			get { return items; }
			set { SetProperty(ref items, value, "Items"); }
		}

		ToDoItem selectedItem;
		public ToDoItem SelectedItem
		{
			get { return selectedItem; }
			set
			{
				SetProperty(ref selectedItem, value, "SelectedItem");
				if (selectedItem != null)
				{
					Application.Current.MainPage.Navigation.PushAsync(new Pages.Tests.PageTaskDetail(selectedItem));
					SelectedItem = null;
				}
			}
		}

		async Task ExecuteRefreshCommand()
		{
			if (IsBusy)
				return;
			IsBusy = true;

			try
			{
				var identity = await cloudService.GetIdentityAsync();
				if (identity != null)
				{
					var name = identity.UserClaims.FirstOrDefault(c => c.Type.Equals("name")).Value;
					Title = $"Tasks for {name}";
				}
				var list = await Table.ReadAllItemsAsync();
				Items.ReplaceRange(list);
			}
			catch (Exception ex)
			{
				await Application.Current.MainPage.DisplayAlert("Items Not Loaded", ex.Message, "OK");
			}
			finally
			{
				IsBusy = false;
			}
		}

		async Task ExecuteAddNewItemCommand()
		{
			if (IsBusy)
				return;
			IsBusy = true;

			try
			{
				await Application.Current.MainPage.Navigation.PushAsync(new Pages.Tests.PageTaskDetail());
			}
			catch (Exception ex)
			{
				await Application.Current.MainPage.DisplayAlert("Item Not Added", ex.Message, "OK");
			}
			finally
			{
				IsBusy = false;
			}
		}

		async Task ExecuteLogoutCommand()
		{
			if (IsBusy)
				return;
			IsBusy = true;

			try
			{
				var cloudService = ServiceLocator.Instance.Resolve<ICloudService>();
				await cloudService.LogoutAsync();
				Application.Current.MainPage = new NavigationPage(new Pages.Login.PageLogin());
			}
			catch (Exception ex)
			{
				await Application.Current.MainPage.DisplayAlert("Logout Failed", ex.Message, "OK");
			}
			finally
			{
				IsBusy = false;
			}
		}

		async Task RefreshList()
		{
			await ExecuteRefreshCommand();
			MessagingCenter.Subscribe<TaskDetailViewModel>(this, "ItemsChanged", async (sender) =>
			{
				await ExecuteRefreshCommand();
			});
		}






		//		public TaskListViewModel()
		//		{
		//			Title = "Task List";
		//#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
		//			RefreshList();
		//#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
		//			items.CollectionChanged += this.OnCollectionChanged;
		//		}

		//		void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		//		{
		//			Debug.WriteLine("[TaskList] OnCollectionChanged: Items have changed");
		//		}

		//		ObservableCollection<ToDoItem> items = new ObservableCollection<ToDoItem>();
		//		public ObservableCollection<ToDoItem> Items
		//		{
		//			get { return items; }
		//			set { SetProperty(ref items, value, "Items"); }
		//		}

		//		ToDoItem selectedItem;
		//		public ToDoItem SelectedItem
		//		{
		//			get { return selectedItem; }
		//			set
		//			{
		//				SetProperty(ref selectedItem, value, "SelectedItem");
		//				if (selectedItem != null)
		//				{
		//					Application.Current.MainPage.Navigation.PushAsync(new Pages.Tests.PageTaskDetail(selectedItem));
		//					SelectedItem = null;
		//				}
		//			}
		//		}

		//		Command refreshCmd;
		//		public Command RefreshCommand => refreshCmd ?? (refreshCmd = new Command(async () => await ExecuteRefreshCommand()));

		//		async Task ExecuteRefreshCommand()
		//		{
		//			if (IsBusy)
		//				return;
		//			IsBusy = true;

		//			try
		//			{

		//				var cloudService = ServiceLocator.Instance.Resolve<ICloudService>();
		//				await cloudService.LoginAsync(User);


		//				var table = App.CloudService.GetTable<ToDoItem>();
		//				var list = await table.ReadAllItemsAsync();
		//				Items.Clear();
		//				foreach (var item in list)
		//					Items.Add(item);
		//			}
		//			catch (Exception ex)
		//			{
		//				Debug.WriteLine($"[TaskList] Error loading items: {ex.Message}");
		//			}
		//			finally
		//			{
		//				IsBusy = false;
		//			}
		//		}

		//		Command addNewCmd;
		//		public Command AddNewItemCommand => addNewCmd ?? (addNewCmd = new Command(async () => await ExecuteAddNewItemCommand()));

		//		async Task ExecuteAddNewItemCommand()
		//		{
		//			if (IsBusy)
		//				return;
		//			IsBusy = true;

		//			try
		//			{
		//				await Application.Current.MainPage.Navigation.PushAsync(new Pages.Tests.PageTaskDetail());
		//			}
		//			catch (Exception ex)
		//			{
		//				Debug.WriteLine($"[TaskList] Error in AddNewItem: {ex.Message}");
		//			}
		//			finally
		//			{
		//				IsBusy = false;
		//			}
		//		}

		//		async Task RefreshList()
		//		{
		//			await ExecuteRefreshCommand();
		//			MessagingCenter.Subscribe<TaskDetailViewModel>(this, "ItemsChanged", async (sender) =>
		//			{
		//				await ExecuteRefreshCommand();
		//			});
		//		}
	}
}
