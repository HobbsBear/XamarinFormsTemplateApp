using System;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using PassTimeSportsService.Models;
using PassTimeSportsService.DataObjects;
using Microsoft.Azure.Mobile.Server.Login;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using Microsoft.Azure.Mobile.Server.Authentication;
using System.Security.Principal;
using System.Data.Entity.Migrations;
using Newtonsoft.Json;

namespace PassTimeSportsService.Controllers
{
	[Route(".auth/login/custom")]
	public class CustomAuthController : ApiController
	{
		private MobileServiceContext dbContext;
		public string Audience { get; set; }
		public string Issuer { get; set; }
		public string SigningKey { get; set; }

		public CustomAuthController()
		{
			dbContext = new MobileServiceContext();
			string website = Environment.GetEnvironmentVariable("WEBSITE_HOSTNAME");
			Audience = $"https://{website}/";
			Issuer = $"https://{website}/";
			SigningKey = Environment.GetEnvironmentVariable("WEBSITE_AUTH_SIGNING_KEY");
		}

		[HttpPost]
		public IHttpActionResult Post([FromBody] User body)
		{
			if (body == null)
			{
				return BadRequest("User not set.");
			}
			else if (body.Username == null || body.Username.Length == 0)
			{
				return BadRequest("Username not set.");
			}
			else if (body.Password == null || body.Password.Length == 0)
			{
				return BadRequest("Password not set.");
			}

			if (!IsValidUser(body))
			{
				return BadRequest("User was not valid");
			}

			var validatedUser = (from u in dbContext.Users where u.Username == body.Username && u.Password == body.Password select u).FirstOrDefault();

			// Mind a new token based on the old one plus the new information
			var claims = new Claim[]
			{
				new Claim(JwtRegisteredClaimNames.Sub, body.Username),
				new Claim(JwtRegisteredClaimNames.FamilyName, validatedUser.LastName),
				new Claim(JwtRegisteredClaimNames.GivenName, validatedUser.FirstName)
			};

			JwtSecurityToken token = AppServiceLoginHandler.CreateToken(claims, SigningKey, Audience, Issuer, TimeSpan.FromDays(30));

			// Return the token and user ID to the client
			return Ok(new LoginResult()
			{
				AuthenticationToken = token.RawData,
				User = new LoginResultUser {
					UserId = validatedUser.Username,
					firstName = validatedUser.FirstName,
					lastName = validatedUser.LastName
				}
			});
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				dbContext.Dispose();
			}
			base.Dispose(disposing);
		}

		//[HttpGet]
		//public async Task<IHttpActionResult> Get()
		//{
		//	var creds = await User.GetAppServiceIdentityAsync<AzureActiveDirectoryCredentials>(Request);
		//	var sid = ((ClaimsPrincipal)User).FindFirst(ClaimTypes.NameIdentifier).Value;

		//	string claimFirstName, claimLastName, claimEmailAddress, claimPassword, claimUsername;
		//	try
		//	{
		//		claimEmailAddress = creds.UserId;
		//		claimFirstName = creds.UserClaims.FirstOrDefault(claim => claim.Type.Equals("firstname")).Value;
		//		claimLastName = creds.UserClaims.FirstOrDefault(claim => claim.Type.Equals("lastname")).Value;
		//		claimPassword = creds.UserClaims.FirstOrDefault(claim => claim.Type.Equals("password")).Value;
		//		claimUsername = creds.UserClaims.FirstOrDefault(claim => claim.Type.Equals("username")).Value;
		//	}
		//	catch (Exception)
		//	{
		//		return BadRequest("Email or Name is not present");
		//	}

		//	try
		//	{
		//		// Insert the record information into the database
		//		User user = new User()
		//		{
		//			UserId = 0,
		//			FirstName = claimFirstName,
		//			LastName = claimLastName,
		//			Password = claimPassword,
		//			Username = claimUsername,
		//			EmailAddress = claimEmailAddress
		//		};
		//		dbContext.Users.AddOrUpdate(user);
		//		dbContext.SaveChanges();
		//	}
		//	catch (DbUpdateException ex)
		//	{
		//		return InternalServerError(ex);
		//	}

		//	// Mind a new token based on the old one plus the new information
		//	var newClaims = new Claim[]
		//	{
		//		new Claim(JwtRegisteredClaimNames.Sub, sid.ToString()),
		//		new Claim(JwtRegisteredClaimNames.FamilyName, claimLastName),
		//		new Claim(JwtRegisteredClaimNames.GivenName, claimFirstName)
		//	};
		//	JwtSecurityToken token = AppServiceLoginHandler.CreateToken(
		//	    newClaims, SigningKey, Audience, Issuer, TimeSpan.FromDays(30));

		//	// Return the token and user ID to the client
		//	return Ok(new LoginResult()
		//	{
		//		AuthenticationToken = token.RawData,
		//		User = new LoginResultUser {
		//			UserId = claimUsername,
		//			firstName = claimFirstName,
		//			lastName = claimLastName
		//		}
		//	});
		//}

		private bool IsValidUser(User user)
		{
			return dbContext.Users.Count(u => u.Username.Equals(user.Username) && u.Password.Equals(user.Password)) > 0;
		}

		public class LoginResult
		{
			[JsonProperty(PropertyName = "authenticationToken")]
			public string AuthenticationToken { get; set; }

			[JsonProperty(PropertyName = "user")]
			public LoginResultUser User { get; set; }
		}

		public class LoginResultUser
		{
			[JsonProperty(PropertyName = "userId")]
			public string UserId { get; set; }
			[JsonProperty(PropertyName = "firstName")]
			public string firstName { get; set; }
			[JsonProperty(PropertyName = "lastName")]
			public string lastName { get; set; }
		}
	}
}