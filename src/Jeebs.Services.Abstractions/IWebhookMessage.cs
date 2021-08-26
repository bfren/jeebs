﻿// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs.Services.Webhook
{
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
}