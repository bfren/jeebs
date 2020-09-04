using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Config;
using Jeebs.Services.Webhook;

namespace Jeebs.Services.Drivers.Webhook.RocketChat.Models
{
	/// <summary>
	/// RocketChat message
	/// </summary>
	public sealed class RocketChatMessage
	{
		/// <summary>
		/// Message alias - will be set to the Application name
		/// </summary>
		public string Alias { get; set; }

		/// <summary>
		/// [Optional] Message text - message can be set using attachments (which support message levels)
		/// </summary>
		public string? Text { get; set; }

		/// <summary>
		/// Attachments
		/// </summary>
		public List<RocketChatAttachment> Attachments { get; set; } = new List<RocketChatAttachment>();

		/// <summary>
		/// Create a message
		/// </summary>
		/// <param name="config">JeebsConfig</param>
		/// <param name="text">Message text</param>
		/// <param name="level">MessageLevel</param>
		public RocketChatMessage(JeebsConfig config, string text, MessageLevel level)
		{
			Alias = config.App.Name;
			Attachments = new List<RocketChatAttachment>
			{
				{ new RocketChatAttachment(text, level) }
			};
		}
	}
}
