using TemplateApp.Abstractions;

namespace TemplateApp.BusinessLayer.Models
{
	public class User : TableData
	{
		public int UserId { get; set; }

		public string Username { get; set; }

		public string Password { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string EmailAddress { get; set; }
		
		public byte[] Image { get; set; }
		

	}
}
