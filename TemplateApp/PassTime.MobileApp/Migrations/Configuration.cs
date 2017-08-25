namespace PassTime.MobileApp.Migrations
{
	using DataObjects;
	using System;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.Linq;

	internal sealed class Configuration : DbMigrationsConfiguration<PassTime.MobileApp.Models.MobileServiceContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = false;
		}

		protected override void Seed(PassTime.MobileApp.Models.MobileServiceContext context)
		{
			User Hobbs = new User()
			{
				UserID = 0,
				Username = "HobbsBear",
				EmailAddress = "chrishobbs19@gmail.com",
				Password = "Password1",
				Birthday = new DateTime(1992, 4, 30),
				FirstName = "Chris",
				LastName = "Hobbs",
				Picture = null
			};

			User Kenney = new User()
			{
				UserID = 1,
				Username = "CKenney",
				EmailAddress = "ckenney98@gmail.com",
				Password = "Password1",
				Birthday = new DateTime(1992, 4, 30),
				FirstName = "Chris",
				LastName = "Kenney",
				Picture = null
			};

			User Brandon = new User()
			{
				UserID = 2,
				Username = "BKrause",
				EmailAddress = "bkrause@hastings.edu",
				Password = "Password1",
				Birthday = new DateTime(1992, 4, 30),
				FirstName = "Brandon",
				LastName = "Krause",
				Picture = null
			};

			User Jamaane = new User()
			{
				UserID = 3,
				Username = "JamaaneJ",
				EmailAddress = "jdotjordan@gmail.com",
				Password = "Password1",
				Birthday = new DateTime(1992, 4, 30),
				FirstName = "Jamaane",
				LastName = "Jordan",
				Picture = null
			};

			context.Users.AddOrUpdate(Hobbs, Kenney, Brandon, Jamaane);

			Event event1 = new Event()
			{
				EventID = 0,
				HostUser = Brandon,
				Description = "Basketball at the Fallbrook YMCA",
				NumberOfUsers = 10,
				RegisteredUsers = new List<User> {Hobbs, Kenney, Brandon, Jamaane },
				StartDate = new DateTime(2017, 2, 20, 20, 0, 0),
				EndDate = new DateTime(2017, 2, 20, 22, 0, 0)
			};

			Event event2 = new Event()
			{
				EventID = 0,
				HostUser = Hobbs,
				Description = "Dungeons and Dragons",
				NumberOfUsers = 10,
				RegisteredUsers = new List<User> { Hobbs, Kenney },
				StartDate = new DateTime(2017, 2, 19, 12, 0, 0),
				EndDate = new DateTime(2017, 2, 19, 22, 0, 0)
			};

			context.Events.AddOrUpdate(event1, event2);
		}
	}
}
