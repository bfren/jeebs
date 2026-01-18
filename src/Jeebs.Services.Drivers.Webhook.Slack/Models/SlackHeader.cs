// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Text.Json.Serialization;

namespace Jeebs.Services.Drivers.Webhook.Slack.Models;

/// <summary>
/// Slack message block - header.
/// </summary>
public sealed record class SlackHeader : SlackBlock
{
	/// <summary>
	/// Header text.
	/// </summary>
	[JsonPropertyName("text")]
	public SlackContent Text { get; init; }

	/// <summary>
	/// Create object.
	/// </summary>
	/// <param name="text">Header text.</param>
	public SlackHeader(SlackContent text) : base("header") =>
		Text = text;
}
