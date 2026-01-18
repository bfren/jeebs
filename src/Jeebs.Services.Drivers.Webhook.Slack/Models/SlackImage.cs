// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Text.Json.Serialization;

namespace Jeebs.Services.Drivers.Webhook.Slack.Models;

/// <summary>
/// Slack message block content accessory - image.
/// </summary>
public sealed record class SlackImage : SlackAccessory
{
	/// <summary>
	/// Image URL
	/// </summary>
	[JsonPropertyName("image_url")]
	public string Url { get; private init; }

	/// <summary>
	/// Image alt text
	/// </summary>
	[JsonPropertyName("alt_text")]
	public string Alt { get; private init; }

	/// <summary>
	/// Create object
	/// </summary>
	/// <param name="url">Image URL</param>
	/// <param name="alt">Image alt text</param>
	public SlackImage(string url, string alt) : base("image") =>
		(Url, Alt) = (url, alt);
}
