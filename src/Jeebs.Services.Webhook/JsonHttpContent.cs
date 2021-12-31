// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Net.Http;
using System.Text;
using static F.JsonF;

namespace Jeebs.Services.Webhook;

/// <summary>
/// JSON-encoded HTTP content
/// </summary>
public class JsonHttpContent : StringContent
{
	/// <summary>
	/// Encode object as JSON and set media type to 'application/json'
	/// </summary>
	/// <param name="obj">Content to be encoded</param>
	public JsonHttpContent(object obj) : this(obj, "application/json") { }

	/// <summary>
	/// Encode object as JSON
	/// </summary>
	/// <param name="obj">Content to be encoded</param>
	/// <param name="type">Content-type</param>
	public JsonHttpContent(object obj, string type) : base(Serialise(obj).Unwrap(string.Empty), Encoding.UTF8, type) { }
}
