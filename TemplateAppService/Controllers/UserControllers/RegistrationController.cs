using System.Web.Http;
using Microsoft.Azure.Mobile.Server.Config;
using TemplateAppService.Models;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Collections.Generic;
using System.Net;
using System;
using System.Net.Http;
using System.Linq;

namespace TemplateAppService.Controllers
{
    [MobileAppController]
    public class RegistrationController : ApiController
    {
		private MobileServiceContext dbContext;

		//10001110101 - The Registration Controller really shouldn't exist. This registration should be a POST method in the UserController. Ya-Duh.
		public RegistrationController()
		{
			dbContext = new MobileServiceContext();
		}

		//// GET api/Registration
		//public string Get(string email, string first, string last, string pass, string username)
		//{

		//	try
		//	{
		//		// Insert the record information into the database
		//		User user = new User()
		//		{
		//			EmailAddress = email,
		//			FirstName = first,
		//			LastName = last,
		//			Password = pass,
		//			Username = username
		//		};
				
		//		user.UserTypes.Add(UserType.StandardUser);

		//		dbContext.Users.AddOrUpdate(user);
		//		dbContext.SaveChanges();
		//	}
		//	catch (Exception ex)
		//	{
		//		throw new HttpResponseException(HttpStatusCode.BadRequest);
		//	}
		//	return "";
		//}


		public RegistrationResultViewModel Get()
		{
			string First = Tools.IO.GetParameter(Request, "first"),
			     Last = Tools.IO.GetParameter(Request, "last"),
			     Pass = Tools.IO.GetParameter(Request, "pass"),
			     Username = Tools.IO.GetParameter(Request, "username"),
			     Email = Tools.IO.GetParameter(Request, "email");

			RegistrationResultViewModel results = new RegistrationResultViewModel()
			{
				first = First,
				last = Last,
				pass = Pass,
				username = Username,
				email = Email
			};

			

			bool userExists = (from u in dbContext.Users where u.Username.ToLower() == Username.ToLower() || u.EmailAddress.ToLower() == Email.ToLower() select u).Any();

			if (userExists)
			{
				var resp = new HttpResponseMessage(HttpStatusCode.Conflict)
				{
				    Content = new StringContent("Username or Email already exists on the server. Please try again")
				};
				throw new HttpResponseException(resp);
			}
			else
			{
				try
				{
					User user = new User()
					{
						EmailAddress = results.email,
						FirstName = results.first,
						LastName = results.last,
						Password = results.pass,
						Username = results.username
					};

					user.UserTypes = new List<UserType>();
					user.UserTypes.Add(UserType.StandardUser);

					dbContext.Users.AddOrUpdate(user);
					dbContext.SaveChanges();
					results.Result = user.UserId.ToString();
				}
				catch (Exception ex)
				{
				    throw ex;// new HttpResponseException(HttpStatusCode.BadRequest);
				}

				return results;
			}
		}
	
		public class RegistrationResultViewModel
		{
			public string first { get; set; }
			public string last { get; set; }
			public string pass { get; set; }
			public string username { get; set; }
			public string email { get; set; }
			public string Result { get; set; }
		}


	}
}
