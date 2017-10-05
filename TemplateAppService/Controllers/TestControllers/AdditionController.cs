using System.Web.Http;
using Microsoft.Azure.Mobile.Server.Config;
using System.Net;
using System.Net.Http;
using System.Linq;
using System;

namespace PassTimeSportsService.Controllers
{
	[MobileAppController]
	public class AdditionController : ApiController
	{
        // GET api/Addition
		public ResultViewModel Get()
		{
			int? first = GetParameter(Request, "first"),
				second = GetParameter(Request, "second");

			ResultViewModel results = new ResultViewModel()
			{
				First = first.GetValueOrDefault(),
				Second = second.GetValueOrDefault() 
			};
			results.Result = results.First + results.Second;
			return results;
		}
		public class ResultViewModel
		{
			public int First { get; set; }
			public int Second { get; set; }
			public int Result { get; set; }
		}
		private int? GetParameter(HttpRequestMessage request, string name)
		{
			var queryParams = request.GetQueryNameValuePairs().Where(kv => kv.Key == name).ToList();
			if (queryParams.Count == 0)
			{
				throw new HttpResponseException(HttpStatusCode.BadRequest);
			}

			int rv;
			if (!Int32.TryParse(queryParams[0].Value, out rv))
			{
				throw new HttpResponseException(HttpStatusCode.BadRequest);
			}
			return rv;
		}
	}
}
