// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

using System.Text.Json.Serialization;

namespace Jeebs.Services.Drivers.Webhook.Slack.Models
{
	/// <summary>
	/// Slack attachment
	/// </summary>
	public sealed record SlackAttachment
	{
		/// <summary>
		/// Attachment text
		/// </summary>
		public string Text { get; private init; }

		/// <summary>
		/// Attachment colour
		/// </summary>
		[JsonPropertyName("color")]
		public string Colour { get; private init; }

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="text">Attachment text</param>
		/// <param name="level">Message level</param>
		public SlackAttachment(string text, NotificationLevel level) =>
			(Text, Colour) = (text, GetColour(level));

		private static string GetColour(NotificationLevel level) =>
			level switch
			{
				NotificationLevel.Information =>
					"good",

				NotificationLevel.Warning =>
					"warning",

				NotificationLevel.Error =>
					"danger",

				_ =>
					throw new Jx.Services.Webhook.UnknownMessageLevelException()
			};
	}
}
