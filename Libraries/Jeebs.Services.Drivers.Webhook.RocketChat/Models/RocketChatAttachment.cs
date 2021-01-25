using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Jeebs.Services.Drivers.Webhook.RocketChat.Models
{
	/// <summary>
	/// RocketChat Attachment
	/// </summary>
	public sealed class RocketChatAttachment
	{
		/// <summary>
		/// Text
		/// </summary>
		public string Text { get; }

		/// <summary>
		/// Colour
		/// </summary>
		[JsonPropertyName("color")]
		public string Colour { get; }

		/// <summary>
		/// [Optional] Attachment title
		/// </summary>
		public string? Title { get; }

		/// <summary>
		/// [Optional] Attachment link
		/// </summary>
		[JsonPropertyName("title_link")]
		public string? TitleLink { get; }

		/// <summary>
		/// Fields
		/// </summary>
		public List<RocketChatAttachmentField> Fields { get; set; } = new List<RocketChatAttachmentField>();

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="text">Attachment text</param>
		/// <param name="level">Message level</param>
		public RocketChatAttachment(string text, NotificationLevel level) =>
			(Text, Colour) = (text, GetColour(level));

		private string GetColour(NotificationLevel level) =>
			level switch
			{
				NotificationLevel.Information =>
					"#2cbe4e", // green

				NotificationLevel.Warning =>
					"#ffc107", // amber

				NotificationLevel.Error =>
					"#cb2431", // red

				_ =>
					throw new Jx.Services.Webhook.UnknownMessageLevelException()
			};
	}
}
