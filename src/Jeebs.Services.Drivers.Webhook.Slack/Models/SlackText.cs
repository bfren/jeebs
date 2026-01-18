// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Text.Json.Serialization;

namespace Jeebs.Services.Drivers.Webhook.Slack.Models;

/// <summary>
/// Slack message block text content.
/// </summary>
public abstract record class SlackText : SlackContent
{
	/// <summary>
	/// Text content
	/// </summary>
	[JsonPropertyName("text")]
	public string Text { get; init; }

	/// <summary>
	/// Create object
	/// </summary>
	/// <param name="type">Content type (e.g. 'mrkdwn')</param>
	/// <param name="text">Text content</param>
	protected SlackText(string type, string text) : base(type) =>
		(Type, Text) = (type, text);
}
