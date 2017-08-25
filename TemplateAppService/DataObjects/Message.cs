using Microsoft.Azure.Mobile.Server;
using System.Collections.Generic;

namespace TemplateAppService.DataObjects
{
	public class Message : EntityData
	{
		public string UserId { get; set; }
		public string Text { get; set; }
	}
}