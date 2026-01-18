// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Text.Json.Serialization;

namespace Jeebs.Services.Drivers.Webhook.Slack.Models;

/// <summary>
/// Slack message block content.
/// </summary>
[JsonDerivedType(typeof(SlackMarkdown))]
[JsonDerivedType(typeof(SlackPlainText))]
public abstract record class SlackContent
{
	/// <summary>
	/// Block content type (e.g. 'text').
	/// </summary>
	[JsonPropertyName("type")]
	public string Type { get; init; }

	/// <summary>
	/// Create object.
	/// </summary>
	/// <param name="type">Block content type</param>
	protected SlackContent(string type) =>
		Type = type;
}
