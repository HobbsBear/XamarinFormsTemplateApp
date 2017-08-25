using System.Web.Http;
using System.Net;
using System.Net.Http;
using System.Linq;

namespace TemplateAppService.Tools
{
	public class IO
	{
		public static string GetParameter(HttpRequestMessage request, string name)
		{
			var queryParams = request.GetQueryNameValuePairs().Where(kv => kv.Key == name).ToList();
			if (queryParams.Count == 0)
			{
				throw new HttpResponseException(HttpStatusCode.BadRequest);
			}
			return queryParams[0].Value;
		}
	}
}