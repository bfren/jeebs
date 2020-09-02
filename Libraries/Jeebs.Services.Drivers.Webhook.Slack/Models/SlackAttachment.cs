using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Services.Webhook;

namespace Jeebs.Services.Drivers.Webhook.Slack
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
		public string Color { get; }

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="text">Attachment text</param>
		/// <param name="level">Message level</param>
		public SlackAttachment(string text, MessageLevel level)
			=> (Text, Color) = (text, getColour(level));

		private string getColour(MessageLevel level)
			=> level switch
			{
				MessageLevel.Information => "good",
				MessageLevel.Warning => "warning",
				MessageLevel.Error => "danger",
				_ => throw new NotImplementedException()
			};
	}
}
