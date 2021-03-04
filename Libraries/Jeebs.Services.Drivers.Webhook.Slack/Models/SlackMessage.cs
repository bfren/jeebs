using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Config;

namespace Jeebs.Services.Drivers.Webhook.Slack.Models
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
		public SlackMessage(JeebsConfig config, string text, NotificationLevel level)
		{
			Username = config.App.FullName;
			Attachments = new List<SlackAttachment>
			{
				{ new (text, level) }
			};
		}
	}
}
