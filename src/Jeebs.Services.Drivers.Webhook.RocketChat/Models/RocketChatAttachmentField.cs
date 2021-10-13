// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Services.Drivers.Webhook.RocketChat.Models;

/// <summary>
/// RocketChat Attachment Field
/// </summary>
public sealed record class RocketChatAttachmentField
{
	/// <summary>
	/// Whether or not this is a short field
	/// </summary>
	public bool Short { get; init; }

	/// <summary>
	/// Field title
	/// </summary>
	public string Title { get; private init; }

	/// <summary>
	/// Field value
	/// </summary>
	public string Value { get; private init; }

	/// <summary>
	/// Create object
	/// </summary>
	/// <param name="title">Field title</param>
	/// <param name="value">Field value</param>
	public RocketChatAttachmentField(string title, string value) =>
		(Title, Value) = (title, value);
}
