using Microsoft.Azure.Mobile.Server;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using PassTimeSportsService.DataObjects;

namespace PassTimeSportsService.Models
{
	public class User
	{
        public User()
        {
            RegisteredEvents = new List<Event>();
        }
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int UserId { get; set; }

		public string Username { get; set; }

		//10001110101 -- The needs to be hashed.
		public string Password { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string EmailAddress { get; set; }
		
		public byte[] Image { get; set; }

		public ICollection<UserType> UserTypes { get; set; }

        public virtual ICollection<Event> RegisteredEvents { get; set; }

    }

	public enum UserType
	{
		Administrator,
		LeagueAdmin,
		StandardUser
	}
}