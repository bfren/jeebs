// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Jeebs.Services.Drivers.Webhook.Slack.Models;

/// <summary>
/// Slack message block - section
/// </summary>
public sealed record class SlackSection : SlackBlock
{
	/// <summary>
	/// Optional - text content
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	[JsonPropertyName("text")]
	public SlackText? Text { get; init; }

	/// <summary>
	/// Optional - accessory
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	[JsonPropertyName("accessory")]
	public SlackAccessory? Accessory { get; init; }

	/// <summary>
	/// Optional - fields
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	[JsonPropertyName("fields")]
	public List<SlackContent>? Fields { get; init; }

	/// <summary>
	/// Create empty section
	/// </summary>
	public SlackSection() : base("section") { }

	/// <summary>
	/// Create section with text content
	/// </summary>
	/// <param name="text">Text content</param>
	public SlackSection(SlackText text) : this() =>
		Text = text;

	/// <summary>
	/// Create section with text content plus accessory
	/// </summary>
	/// <param name="text">Text content</param>
	/// <param name="accessory">Accessory content</param>
	public SlackSection(SlackText text, SlackAccessory accessory) : this() =>
		(Text, Accessory) = (text, accessory);

	/// <summary>
	/// Create section with fields
	/// </summary>
	/// <param name="fields">Fields</param>
	public SlackSection(params SlackContent[] fields) : this() =>
		Fields = fields.ToList();
}
