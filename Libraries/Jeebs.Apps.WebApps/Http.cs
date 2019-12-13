using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Jeebs.Apps.WebApps
{
	/// <summary>
	/// HTTP
	/// </summary>
	public static class Http
	{
		/// <summary>
		///	Return this application's HttpClient instance
		/// </summary>
		public static HttpClient Client { get => client.Value; }

		/// <summary>
		///	HttpClient object - should only be created once per application instance
		/// </summary>
		private static readonly Lazy<HttpClient> client = new Lazy<HttpClient>(() => new HttpClient(), true);
	}
}
