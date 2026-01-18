// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Text.Json.Serialization;

namespace Jeebs.Services.Drivers.Webhook.Slack.Models;

/// <summary>
/// Slack message block.
/// </summary>
[JsonDerivedType(typeof(SlackDivider))]
[JsonDerivedType(typeof(SlackHeader))]
[JsonDerivedType(typeof(SlackSection))]
public abstract record class SlackBlock
{
	/// <summary>
	/// Block type (e.g. 'section' or 'divider').
	/// </summary>
	[JsonPropertyName("type")]
	public string Type { get; private init; }

	/// <summary>
	/// Create object.
	/// </summary>
	/// <param name="type">Block type.</param>
	protected SlackBlock(string type) =>
		Type = type;
}
