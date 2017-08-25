using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using TemplateAppService.DataObjects;
using TemplateAppService.Models;
using System.Net.Http;
using System.Net;
using System;
using Microsoft.ApplicationInsights;

namespace TemplateAppService.Controllers
{
	[Authorize]
	public class EventController : TableController<Event>
	{
		MobileServiceContext dbContext;
		protected override void Initialize(HttpControllerContext controllerContext)
		{
			base.Initialize(controllerContext);
			dbContext = new MobileServiceContext();
			DomainManager = new EntityDomainManager<Event>(dbContext, Request, enableSoftDelete: true);
		}

		// GET tables/Event
		public IQueryable<Event> GetAllEvent()
		{
		    return Query(); 
		}

		// GET tables/Event/48D68C86-6EA6-4C25-AA33-223FC9A27959
		public SingleResult<Event> GetEvent(string id)
		{
		    return Lookup(id);
		}

		// PATCH tables/Event/48D68C86-6EA6-4C25-AA33-223FC9A27959
		public Task<Event> PatchEvent(string id, Delta<Event> patch)
		{
			ValidateOwner(id);
			return UpdateAsync(id, patch);
		}

		// POST tables/Event
		public async Task<IHttpActionResult> PostEvent(Event item)
		{
			//item.StartTime = new System.DateTimeOffset(new System.DateTime(2017, 1, 1));
			//item.EndTime = null;
			//item.EventCompetitionLevel = EventCompetitionLevel.Advanced;
			item.EventType = (from type in dbContext.EventTypes where type.Name == "basketball" select type).FirstOrDefault();
			item.EventStatus = (from status in dbContext.EventStatus where status.Status == "valid" select status).FirstOrDefault();
			//item.LengthInMinutes = 200;
			//item.Location = "Lincoln, NE";
			//item.MaxNumberOfAttendees = 20;
			item.HostUser = (from user in dbContext.Users where user.Username == "chris" select user).FirstOrDefault();
			item.HostUserID = item.HostUser.UserId;
			//item.MinNumberOfAttendees = 10;

			try
			{
				Event current = await InsertAsync(item);
				return CreatedAtRoute("Tables", new { id = current.Id }, current);
			}
			catch (HttpResponseException ex)
			{
				string message = ((HttpError)((ObjectContent)ex.Response.Content).Value).First().Value.ToString();
				string[] temp = message.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
				var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
				{
					Content = new StringContent(message + "\r\n" + ex.Source + "\r\n" + ex.StackTrace + "\r\n" + ex.InnerException + ex.Message + ex.Response),
					ReasonPhrase = temp[0]
				};
				throw new HttpResponseException(resp);
			}

		}

		// DELETE tables/Event/48D68C86-6EA6-4C25-AA33-223FC9A27959
		public Task DeleteEvent(string id)
		{
			ValidateOwner(id);
			return DeleteAsync(id);
		}

		public void ValidateOwner(string id)
		{
			var result = Lookup(id);//.Queryable.PerUserFilter(ClaimUserId).FirstOrDefault<TodoItem>();
			if (result == null)
			{
				throw new HttpResponseException(HttpStatusCode.NotFound);
			}
		}
	}
}