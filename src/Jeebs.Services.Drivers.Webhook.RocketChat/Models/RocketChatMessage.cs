// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Config;

namespace Jeebs.Services.Drivers.Webhook.RocketChat.Models;

/// <summary>
/// RocketChat message
/// </summary>
public sealed record class RocketChatMessage
{
	/// <summary>
	/// Message alias - will be set to the Application name
	/// </summary>
	public string Alias { get; private init; }

	/// <summary>
	/// [Optional] Message text - message can be set using attachments (which support message levels)
	/// </summary>
	public string? Text { get; init; }

	/// <summary>
	/// Attachments
	/// </summary>
	public List<RocketChatAttachment> Attachments { get; private init; }

	/// <summary>
	/// Create a message
	/// </summary>
	/// <param name="config">JeebsConfig</param>
	/// <param name="text">Message text</param>
	/// <param name="level">MessageLevel</param>
	public RocketChatMessage(JeebsConfig config, string text, NotificationLevel level)
	{
		Alias = config.App.FullName;
		Attachments = new List<RocketChatAttachment>
		{
			{ new RocketChatAttachment(text, level) }
		};
	}
}
