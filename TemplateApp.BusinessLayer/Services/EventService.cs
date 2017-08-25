using System;
using System.Diagnostics;
using Microsoft.WindowsAzure.MobileServices;
using TemplateApp.Abstractions;
using Xamarin.Forms;
using TemplateApp.BusinessLayer.Models;
using System.Collections.Generic;
using TemplateApp.Helpers;
using System.Threading.Tasks;
using System.Net.Http;

namespace TemplateApp.BusinessLayer.Services
{
	public class EventService
	{
		static MobileServiceClient client;

		public EventService()
		{
			client = new MobileServiceClient(Locations.AppServiceUrl, new AuthenticationDelegatingHandler());

			if (Locations.AlternateLoginHost != null)
				client.AlternateLoginHost = new Uri(Locations.AlternateLoginHost);
		}

		public static Event getEventByID(int EventID)
		{
			return null;
		}
	}
}
