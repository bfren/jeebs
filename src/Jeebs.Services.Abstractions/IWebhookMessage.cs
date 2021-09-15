// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;

namespace Jeebs.Services.Webhook;

/// <summary>
/// Webhook message interface
/// </summary>
public interface IWebhookMessage
{
	/// <summary>
	/// Message content
	/// </summary>
	string Content { get; init; }

	/// <summary>
	/// Message level
	/// </summary>
	NotificationLevel Level { get; init; }

	/// <summary>
	/// Additional fields to send
	/// </summary>
	Dictionary<string, object> Fields { get; init; }
}
