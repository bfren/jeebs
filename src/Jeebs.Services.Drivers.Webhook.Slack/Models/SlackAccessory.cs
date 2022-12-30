// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Text.Json.Serialization;

namespace Jeebs.Services.Drivers.Webhook.Slack.Models;

/// <summary>
/// Slack message block content accessory
/// </summary>
[JsonDerivedType(typeof(SlackImage))]
public abstract record class SlackAccessory
{
	/// <summary>
	/// Accessory type (e.g. 'image')
	/// </summary>
	[JsonPropertyName("type")]
	public string Type { get; private init; }

	/// <summary>
	/// Create object
	/// </summary>
	/// <param name="type">Accessory type</param>
	protected SlackAccessory(string type) =>
		Type = type;
}
