using Microsoft.Azure.Mobile.Server;
using System.ComponentModel.DataAnnotations.Schema;

namespace PassTimeSportsService.DataObjects
{
	public class TodoItem : EntityData
	{
		public string UserId { get; set; }

		public string Text { get; set; }

		public bool Complete { get; set; }
		
	}
}