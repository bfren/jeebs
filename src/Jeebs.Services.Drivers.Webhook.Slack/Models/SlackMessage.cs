// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using System.Globalization;
using System.Text.Json.Serialization;
using Jeebs.Config;
using Jeebs.Services.Notify;

namespace Jeebs.Services.Drivers.Webhook.Slack.Models;

/// <summary>
/// Slack Message
/// </summary>
public sealed record class SlackMessage
{
	/// <summary>
	/// Plain text value of the message
	/// </summary>
	[JsonPropertyName("text")]
	public string Text { get; private init; }

	/// <summary>
	/// Blocks (the formatted content of the message)
	/// </summary>
	[JsonPropertyName("blocks")]
	public List<SlackBlock> Blocks { get; private init; }

	/// <summary>
	/// Create a message
	/// </summary>
	/// <param name="config">JeebsConfig</param>
	/// <param name="text">Message text</param>
	/// <param name="level">MessageLevel</param>
	public SlackMessage(JeebsConfig config, string text, NotificationLevel level)
	{
		// Set plain text (fallback) message content
		Text = text;

		// Create header block
		var headerBlock = new SlackHeader(
			text: new SlackPlainText(config.App.FullName)
		);

		// Create text block
		var textBlock = new SlackPlainText(text);

		// Add image if not an information notification
		if (level == NotificationLevel.Information)
		{
			// Add header and text section
			Blocks = new() { headerBlock, new SlackSection(text: textBlock) };
		}
		else
		{
			// Build notification image URL
			var url = string.Format(
				CultureInfo.InvariantCulture,
				"https://bfren.dev/img/notifications/{0}",
				level.ToString().ToLowerInvariant()
			);

			// Add header and text with image section
			Blocks = new()
			{
				headerBlock,
				new SlackSection(
					text: textBlock,
					accessory:new SlackImage($"{url}.png", $"{level}: {url}.txt")
				)
			};
		}
	}
}
