using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using static Jeebs.Util.Json;

namespace Jeebs.Services.Webhook
{
	/// <summary>
	/// JSON-encoded HTTP content
	/// </summary>
	public class JsonHttpContent : StringContent
	{
		/// <summary>
		/// Encode object as JSON and set media type to 'application/json'
		/// </summary>
		/// <param name="obj">Content to be encoded</param>
		/// <param name="type">Content-type</param>
		public JsonHttpContent(object obj, string type = "application/json") : base(Serialise(obj), Encoding.UTF8, type) { }
	}
}
