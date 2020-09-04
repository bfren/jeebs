using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Services.Webhook;
using Newtonsoft.Json;

namespace Jeebs.Services.Drivers.Webhook.Slack.Models
{
	/// <summary>
	/// Slack attachment
	/// </summary>
	public sealed class SlackAttachment
	{
		/// <summary>
		/// Attachment text
		/// </summary>
		public string Text { get; }

		/// <summary>
		/// Attachment colour
		/// </summary>
		[JsonProperty("color")]
		public string Colour { get; }

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="text">Attachment text</param>
		/// <param name="level">Message level</param>
		public SlackAttachment(string text, MessageLevel level)
			=> (Text, Colour) = (text, GetColour(level));

		private string GetColour(MessageLevel level)
			=> level switch
			{
				MessageLevel.Information => "good",
				MessageLevel.Warning => "warning",
				MessageLevel.Error => "danger",
				_ => throw new Jx.Services.Webhook.UnknownMessageLevelException()
			};
	}
}
