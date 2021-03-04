using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Jeebs.Services.Drivers.Webhook.Seq
{
	/// <summary>
	/// Seq Event
	/// </summary>
	public sealed record SeqEvent
	{
		/// <summary>
		/// Timestamp - ISO 8601 format
		/// </summary>
		[JsonPropertyName("@t")]
		public string Timestamp { get; private init; }

		/// <summary>
		/// Message content
		/// </summary>
		[JsonPropertyName("@m")]
		public string Message { get; private init; }

		/// <summary>
		/// Message level
		/// </summary>
		[JsonPropertyName("@l")]
		public string Level { get; private init; }

		/// <summary>
		/// Create event
		/// </summary>
		/// <param name="message"></param>
		/// <param name="level"></param>
		public SeqEvent(string message, NotificationLevel level)
		{
			Timestamp = DateTime.Now.ToString("O");
			Message = message;
			Level = Enum.GetName(typeof(NotificationLevel), level) ?? nameof(NotificationLevel.Information);
		}
	}
}
