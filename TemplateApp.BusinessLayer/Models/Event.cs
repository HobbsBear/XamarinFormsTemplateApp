using TemplateApp.Abstractions;
using System;

namespace TemplateApp.BusinessLayer.Models
{
	public class Event : TableData
	{
		public string Description { get; set; }
		public string Location { get; set; }
		//public int? MaxNumberOfAttendees { get; set; }
		//public int MinNumberOfAttendees { get; set; }
		public EventType EventType { get; set; }
		public EventCompetitionLevel EventCompetitionLevel { get; set; }
		public EventStatus EventStatus { get; set; }
		public DateTimeOffset StartTime { get; set; }
		//public DateTimeOffset? EndTime { get; set; }
		//public int LengthInMinutes { get; set; }

		public int HostUserID { get; set; }
		
		public User HostUser { get; set; }

	}

	public class EventType : TableData
	{
		public string Name { get; set; }
		public int? DefaultMaxAttendees { get; set; }
		public int DefaultMinAttendees { get; set; }

	}

	public class EventStatus : TableData
	{
		public string Status { get; set; }
	}

	public enum EventCompetitionLevel
	{
		Recreational = 1,
		Competitive = 2,
		Advanced = 3
	}
}