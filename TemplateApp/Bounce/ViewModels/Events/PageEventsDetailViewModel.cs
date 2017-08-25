using System;
using System.Diagnostics;
using System.Threading.Tasks;
using TemplateApp.Abstractions;
using TemplateApp.Helpers;
using TemplateApp.BusinessLayer.Models;
using Xamarin.Forms;
using System.Collections.Generic;

namespace TemplateApp.ViewModels
{
	public class PageEventsDetailViewModel : BaseViewModel
	{
		List<EventCompetitionLevel> levels = new List<EventCompetitionLevel>();
		ICloudService cloudService;

		ObservableRangeCollection<EventType> eventTypes = new ObservableRangeCollection<EventType>();
		public ObservableRangeCollection<EventType> EventTypes
		{
			get { return eventTypes; }
			set { SetProperty(ref eventTypes, value, "EventTypes"); }
		}

		public PageEventsDetailViewModel(Event localEvent = null)
		{
			cloudService = ServiceLocator.Instance.Resolve<ICloudService>();

			setupEventOptions();

			if (localEvent != null)
			{
				Item = localEvent;
				Title = localEvent.Description;
				onFormStartDate = localEvent.StartTime.DateTime;
				onFormStartTime = localEvent.StartTime.TimeOfDay;
			}
			else
			{
				Item = new Event { Description = "New Event"};
				onFormStartTime = new TimeSpan(17, 0, 0);
				onFormStartDate = DateTime.Now;

				Title = "Creating a new game.";
			}

			SaveCommand = new Command(async () => await ExecuteSaveCommand());
			DeleteCommand = new Command(async () => await ExecuteDeleteCommand());
		}

		private async void setupEventOptions()
		{
			EventTable = cloudService.GetTable<Event>();

			levels.Add(EventCompetitionLevel.Advanced);
			levels.Add(EventCompetitionLevel.Competitive);
			levels.Add(EventCompetitionLevel.Recreational);
			
			EventTypesTable = cloudService.GetTable<EventType>();
			var list = await EventTypesTable.ReadAllItemsAsync();
			eventTypes.ReplaceRange(list);
		}

		public Event Item { get; set; }
		public User HostUser { get; set; }
		public TimeSpan onFormStartTime { get; set; }
		public DateTime onFormStartDate { get; set; }
		public ICloudTable<Event> EventTable { get; set; }
		public ICloudTable<EventType> EventTypesTable { get; set; }
		public Command SaveCommand { get; }
		public Command DeleteCommand { get; }
		
		async Task ExecuteSaveCommand()
		{
			if (IsBusy)
				return;
			IsBusy = true;
			
			Item.StartTime = new DateTimeOffset(onFormStartDate.Year, onFormStartDate.Month, onFormStartDate.Day, onFormStartTime.Hours, onFormStartTime.Minutes, onFormStartTime.Seconds, onFormStartTime);

			Item.EventType = null;

			try
			{
				if (Item.Id == null)
				{
					await EventTable.CreateItemAsync(Item);
				}
				else
				{
					await EventTable.UpdateItemAsync(Item);
				}
				MessagingCenter.Send<PageEventsDetailViewModel>(this, "EventsChanged");
				await Application.Current.MainPage.Navigation.PopAsync();
			}
			catch (Exception ex)
			{
				await Application.Current.MainPage.DisplayAlert("Save Event Failed", ex.Message, "OK");
			}
			finally
			{
				IsBusy = false;
			}
		}

		async Task ExecuteDeleteCommand()
		{
			if (IsBusy)
				return;
			IsBusy = true;

			try
			{
				if (Item.Id != null)
				{
					await EventTable.UpdateItemAsync(Item);
				}
				MessagingCenter.Send<PageEventsDetailViewModel>(this, "EventsChanged");
				await Application.Current.MainPage.Navigation.PopAsync();
			}
			catch (Exception ex)
			{
				await Application.Current.MainPage.DisplayAlert("Delete Event Failed", ex.Message, "OK");
			}
			finally
			{
				IsBusy = false;
			}
		}
	}
}
