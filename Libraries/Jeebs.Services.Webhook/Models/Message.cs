// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;

namespace Jeebs.Services.Webhook.Models
{
	/// <summary>
	/// Message model
	/// </summary>
	public sealed record Message : IWebhookMessage
	{
		/// <inheritdoc/>
		public string Content { get; init; } = string.Empty;

		/// <inheritdoc/>
		public NotificationLevel Level { get; init; }

		/// <inheritdoc/>
		public Dictionary<string, object> Fields { get; init; } = new();
	}
}
