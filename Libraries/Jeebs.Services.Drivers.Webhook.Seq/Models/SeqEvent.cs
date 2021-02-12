using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Jeebs.Services.Drivers.Webhook.Seq
{
	/// <summary>
	/// Seq Event
	/// </summary>
	public sealed class SeqEvent
	{
		/// <summary>
		/// Timestamp - ISO 8601 format
		/// </summary>
		[JsonPropertyName("@t")]
		public string Timestamp { get; set; }

		/// <summary>
		/// Message content
		/// </summary>
		[JsonPropertyName("@m")]
		public string Message { get; set; }

		/// <summary>
		/// Message level
		/// </summary>
		[JsonPropertyName("@l")]
		public string Level { get; set; }

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
