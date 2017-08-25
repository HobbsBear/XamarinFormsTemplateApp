using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PassTime.MobileApp.DataObjects
{
	public class Event : EntityData
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int EventID { get; set; }
		public User HostUser { get; set; }
		public string Description { get; set; }
		public int NumberOfUsers { get; set; }
		public ICollection<User> RegisteredUsers { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime? EndDate { get; set; }
	}
}