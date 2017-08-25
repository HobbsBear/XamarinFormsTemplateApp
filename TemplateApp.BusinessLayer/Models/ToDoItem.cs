using TemplateApp.Abstractions;

namespace TemplateApp.BusinessLayer.Models
{
	public class ToDoItem : TableData
	{
		public string Text { get; set; }
		public bool Complete { get; set; }
	}
}