using TemplateAppService.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TemplateAppService.DataObjects
{
	public class RegisteredEvents
	{
		[Key, Column(Order = 0)]
		public int UserID { get; set; }

		[ForeignKey("UserID")]
		public User User { get; set; }

		[Key, Column(Order = 1)]
		public string EventID { get; set; }

		[ForeignKey("EventID")]
		public Event Event { get; set; }

		public bool isActive { get; set; }
		public DateTimeOffset? UpdatedAt { get; set; }
		public DateTimeOffset? CreatedAt { get; set; }
	}
}