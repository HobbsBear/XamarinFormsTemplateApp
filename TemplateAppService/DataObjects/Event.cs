using System;
using Microsoft.Azure.Mobile.Server;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using PassTimeSportsService.Models;

namespace PassTimeSportsService.DataObjects
{
	public class Event : EntityData
	{
        public Event()
        {
            RegisteredUsers = new List<User>();
        }

		public string Description { get; set; }
		public string Location { get; set; }
		//public int? MaxNumberOfAttendees { get; set; }
		//public int MinNumberOfAttendees { get; set; }
		public EventType EventType { get; set; }
		//public EventCompetitionLevel EventCompetitionLevel { get; set; }
		public EventStatus EventStatus { get; set; }
		public DateTimeOffset StartTime { get; set; }
		//public DateTimeOffset? EndTime { get; set; }
		//public int LengthInMinutes { get; set; }
		
		public int HostUserID { get; set; }

		[ForeignKey("HostUserID")]
		public User HostUser { get; set; }

        public virtual ICollection<User> RegisteredUsers { get; set; }
    }

	public class EventType : EntityData
	{
		public string Name { get; set; }
		public int? DefaultMaxAttendees { get; set; }
		public int DefaultMinAttendees { get; set; }

	}

	public class EventStatus : EntityData
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