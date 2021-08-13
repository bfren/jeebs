// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Collections.Generic;

namespace Jeebs.Services.Webhook.Models
{
	/// <summary>
	/// Message model
	/// </summary>
	public sealed record class Message : IWebhookMessage
	{
		/// <inheritdoc/>
		public string Content { get; init; } = string.Empty;

		/// <inheritdoc/>
		public NotificationLevel Level { get; init; }

		/// <inheritdoc/>
		public Dictionary<string, object> Fields { get; init; } = new();
	}
}
