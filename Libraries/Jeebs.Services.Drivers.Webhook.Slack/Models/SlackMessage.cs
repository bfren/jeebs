using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Config;
using Jeebs.Services.Webhook;

namespace Jeebs.Services.Drivers.Webhook.Slack
{
	/// <summary>
	/// Slack Message
	/// </summary>
	public sealed class SlackMessage
	{
		/// <summary>
		/// Username (filled with application name)
		/// </summary>
		public string Username { get; set; }

		/// <summary>
		/// Attachments (the actual text of the message)
		/// </summary>
		public List<SlackAttachment> Attachments { get; set; }

		/// <summary>
		/// Create a message
		/// </summary>
		/// <param name="config">JeebsConfig</param>
		/// <param name="text">Message text</param>
		/// <param name="level">MessageLevel</param>
		public SlackMessage(JeebsConfig config, string text, MessageLevel level)
		{
			Username = config.App.Name;
			Attachments = new List<SlackAttachment>(new[]
			{
				new SlackAttachment(text, level)
			});
		}
	}
}
