namespace TemplateAppService.Migrations
{
	using DataObjects;
	using Microsoft.Azure.Mobile.Server.Tables;
	using Models;
	using System;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.Linq;

	internal sealed class Configuration : DbMigrationsConfiguration<TemplateAppService.Models.MobileServiceContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
		SetSqlGenerator("System.Data.SqlClient", new EntityTableSqlGenerator());
	}

        protected override void Seed(TemplateAppService.Models.MobileServiceContext context)
        {
			//List<TodoItem> todoItems = new List<TodoItem>
			//{
			//	new TodoItem { Id = Guid.NewGuid().ToString(), Text = "First item", Complete = false },
			//	new TodoItem { Id = Guid.NewGuid().ToString(), Text = "Second item", Complete = false }
			//};

			//List<User> users = new List<User>
			//{
			//	new User { Username = "chris", Password = "supersecret", EmailAddress="chrishobbs19@gmail.com", FirstName="chris", LastName="hobbs" }
			//};

			//foreach (User user in users)
			//{
			//	context.Set<User>().Add(user);
			//}

			//foreach (TodoItem todoItem in todoItems)
			//{
			//	context.Set<TodoItem>().Add(todoItem);
			//}

			//List <EventType> eventTypes = new List<EventType> {
			//	new EventType { Id = Guid.NewGuid().ToString(), Name = "Football", DefaultMaxAttendees = 40, DefaultMinAttendees = 10 },
			//	new EventType { Id = Guid.NewGuid().ToString(), Name = "Basketball", DefaultMaxAttendees = 20, DefaultMinAttendees = 4 },
			//	new EventType { Id = Guid.NewGuid().ToString(), Name = "Soccer", DefaultMaxAttendees = 40, DefaultMinAttendees = 10 },
			//};

			//List<EventStatus> eventStatus = new List<EventStatus> {
			//	new EventStatus { Id = Guid.NewGuid().ToString(), Status = "Valid" },
			//	new EventStatus { Id = Guid.NewGuid().ToString(), Status = "Cancelled" },
			//	new EventStatus { Id = Guid.NewGuid().ToString(), Status = "Rained Out" },
			//};
			//foreach (EventStatus todoItem in eventStatus)
			//{
			//	context.Set<EventStatus>().Add(todoItem);
			//}
			//foreach (EventType todoItem in eventTypes)
			//{
			//	context.Set<EventType>().Add(todoItem);
			//}

			//base.Seed(context);
		}
	}
}
