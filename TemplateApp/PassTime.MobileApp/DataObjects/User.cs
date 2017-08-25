using System;
using Microsoft.Azure.Mobile.Server;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PassTime.MobileApp.DataObjects
{
	public class User : EntityData
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int UserID { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public DateTime Birthday { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string EmailAddress { get; set; }
		public byte[] Picture { get; set; }
	}
}