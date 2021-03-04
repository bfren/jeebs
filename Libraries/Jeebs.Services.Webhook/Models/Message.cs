using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Services.Webhook.Models
{
	/// <summary>
	/// Message model
	/// </summary>
	public sealed record Message
	{
		/// <summary>
		/// Message content
		/// </summary>
		public string Content { get; init; } = string.Empty;

		/// <summary>
		/// Message level
		/// </summary>
		public NotificationLevel Level { get; init; }

		/// <summary>
		/// Additional fields to send
		/// </summary>
		public Dictionary<string, object> Fields { get; init; } = new();
	}
}
