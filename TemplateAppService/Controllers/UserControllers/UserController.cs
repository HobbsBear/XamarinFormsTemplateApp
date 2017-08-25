using System.Web.Http;
using Microsoft.Azure.Mobile.Server.Config;
using TemplateAppService.Models;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace TemplateAppService.Controllers
{
	[MobileAppController]
	public class UserController : ApiController
	{
		private MobileServiceContext dbContext;

		public UserController()
		{
			dbContext = new MobileServiceContext();
		}

		// GET api/User
		[HttpGet]
		public async Task<User> Get()
		{
			int userID = Convert.ToInt32(Tools.IO.GetParameter(Request, "userID"));

			return (from u in dbContext.Users where u.UserId == userID select u).FirstOrDefault();
		}
	}
}
