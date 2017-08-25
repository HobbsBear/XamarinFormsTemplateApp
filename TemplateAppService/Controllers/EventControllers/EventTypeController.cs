using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using TemplateAppService.DataObjects;
using TemplateAppService.Models;

namespace TemplateAppService.Controllers
{
    public class EventTypeController : TableController<EventType>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<EventType>(context, Request);
        }

        // GET tables/EventType
        public IQueryable<EventType> GetAllEventType()
        {
            return Query(); 
        }

        // GET tables/EventType/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<EventType> GetEventType(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/EventType/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<EventType> PatchEventType(string id, Delta<EventType> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/EventType
        public async Task<IHttpActionResult> PostEventType(EventType item)
        {
            EventType current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/EventType/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteEventType(string id)
        {
             return DeleteAsync(id);
        }
    }
}
