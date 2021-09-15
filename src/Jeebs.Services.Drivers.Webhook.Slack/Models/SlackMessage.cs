// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using Jeebs.Config;

namespace Jeebs.Services.Drivers.Webhook.Slack.Models;

/// <summary>
/// Slack Message
/// </summary>
public sealed record class SlackMessage
{
	/// <summary>
	/// Username (filled with application name)
	/// </summary>
	public string Username { get; private init; }

	/// <summary>
	/// Attachments (the actual text of the message)
	/// </summary>
	public List<SlackAttachment> Attachments { get; private init; }

	/// <summary>
	/// Create a message
	/// </summary>
	/// <param name="config">JeebsConfig</param>
	/// <param name="text">Message text</param>
	/// <param name="level">MessageLevel</param>
	public SlackMessage(JeebsConfig config, string text, NotificationLevel level)
	{
		Username = config.App.FullName;
		Attachments = new List<SlackAttachment>
		{
			{ new (text, level) }
		};
	}
}
