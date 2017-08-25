using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TemplateApp.Abstractions;
using TemplateApp.Helpers;
using TemplateApp.BusinessLayer.Models;
using Xamarin.Forms;

namespace TemplateApp.ViewModels.General
{
	public class PageMainViewModel : BaseViewModel
	{
		ICloudService cloudService;
		public ICloudTable<Event> Table { get; set; }
		public Command RefreshCommand { get; }
		public Command AddNewEventCommand { get; }
		public Command LogoutCommand { get; }
		
		private User user;


		public PageMainViewModel(User user)
		{
			cloudService = ServiceLocator.Instance.Resolve<ICloudService>();
			Table = cloudService.GetTable<Event>();

			Title = "Main List";
			events.CollectionChanged += this.OnCollectionChanged;

			RefreshCommand = new Command(async () => await ExecuteRefreshCommand());
			AddNewEventCommand = new Command(async () => await ExecuteAddNewEventCommand());
			LogoutCommand = new Command(async () => await ExecuteLogoutCommand());

			// Execute the refresh command
			RefreshCommand.Execute(null);
		}

		void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			Debug.WriteLine("[EventsList] OnCollectionChanged: Events have changed");
		}

		ObservableRangeCollection<Event> events = new ObservableRangeCollection<Event>();
		public ObservableRangeCollection<Event> Events
		{
			get { return events; }
			set { SetProperty(ref events, value, "Events"); }
		}

		Event selectedEvent;
		public Event SelectedEvent
		{
			get { return selectedEvent; }
			set
			{
				SetProperty(ref selectedEvent, value, "SelectedEvent");
				if (selectedEvent != null)
				{
					Application.Current.MainPage.Navigation.PushAsync(new Pages.Events.PageEventDetail(selectedEvent));
					SelectedEvent = null;
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
					Title = $"Events for {name}";
				}
				var list = await Table.ReadAllItemsAsync();
				Events.ReplaceRange(list);
			}
			catch (Exception ex)
			{
				await Application.Current.MainPage.DisplayAlert("Events Not Loaded", ex.Message, "OK");
			}
			finally
			{
				IsBusy = false;
			}
		}

		async Task ExecuteAddNewEventCommand()
		{
			if (IsBusy)
				return;
			IsBusy = true;

			try
			{
				await Application.Current.MainPage.Navigation.PushAsync(new Pages.Events.PageEventDetail());
			}
			catch (Exception ex)
			{
				await Application.Current.MainPage.DisplayAlert("Event Not Added", ex.Message, "OK");
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
			MessagingCenter.Subscribe<TaskDetailViewModel>(this, "EventsChanged", async (sender) =>
			{
				await ExecuteRefreshCommand();
			});
		}
		
		async Task ExecuteTestCommand()
		{
			if (IsBusy)
				return;
			IsBusy = true;

			try
			{
				Application.Current.MainPage = new NavigationPage(new Pages.Tests.PageTaskList());
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"[ExecuteLoginCommand] Error = {ex.Message}");
			}
			finally
			{
				IsBusy = false;
			}
		}
		async Task ExecuteEventsNavCommand()
		{
			if (IsBusy)
				return;
			IsBusy = true;

			try
			{
				Application.Current.MainPage = new NavigationPage(new Pages.Events.PageEventList());
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"[ExecuteLoginCommand] Error = {ex.Message}");
			}
			finally
			{
				IsBusy = false;
			}
		}
	}
}
