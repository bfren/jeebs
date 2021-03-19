// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Net.Http;
using System.Text;
using static F.JsonF;

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
		public JsonHttpContent(object obj, string type = "application/json") :
			base(Serialise(obj).Unwrap(string.Empty), Encoding.UTF8, type)
		{ }
	}
}
