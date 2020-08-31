using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Services.Webhook;
using Newtonsoft.Json;

namespace Jeebs.Services.Drivers.Webhook.Seq.Models
{
	/// <summary>
	/// Seq Event
	/// </summary>
	public sealed class Event
	{
		/// <summary>
		/// Timestamp - ISO 8601 format
		/// </summary>
		[JsonProperty("@t")]
		public string Timestamp { get; set; }

		/// <summary>
		/// Message content
		/// </summary>
		[JsonProperty("@m")]
		public string Message { get; set; }

		/// <summary>
		/// Message level
		/// </summary>
		[JsonProperty("@l")]
		public string Level { get; set; }

		/// <summary>
		/// Create event
		/// </summary>
		/// <param name="message"></param>
		/// <param name="level"></param>
		public Event(string message, MessageLevel level)
		{
			Timestamp = DateTime.Now.ToString("O");
			Message = message;
			Level = Enum.GetName(typeof(MessageLevel), level);
		}
	}
}
