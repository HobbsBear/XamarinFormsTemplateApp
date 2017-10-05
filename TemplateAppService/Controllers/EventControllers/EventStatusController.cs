using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using PassTimeSportsService.DataObjects;
using PassTimeSportsService.Models;

namespace PassTimeSportsService.Controllers
{
    public class EventStatusController : TableController<EventStatus>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<EventStatus>(context, Request);
        }

        // GET tables/EventStatus
        public IQueryable<EventStatus> GetAllEventStatus()
        {
            return Query(); 
        }

        // GET tables/EventStatus/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<EventStatus> GetEventStatus(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/EventStatus/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<EventStatus> PatchEventStatus(string id, Delta<EventStatus> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/EventStatus
        public async Task<IHttpActionResult> PostEventStatus(EventStatus item)
        {
            EventStatus current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/EventStatus/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteEventStatus(string id)
        {
             return DeleteAsync(id);
        }
    }
}
