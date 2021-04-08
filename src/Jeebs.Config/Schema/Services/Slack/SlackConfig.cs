// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

namespace Jeebs.Config
{
	/// <summary>
	/// Slack configuration
	/// </summary>
	public record SlackConfig : WebhookServiceConfig
	{
		/// <summary>
		/// Whether or not to add attachments to a message (error type and timestamp)
		/// </summary>
		public bool ShowAttachments { get; init; }

		/// <inheritdoc/>
		public override bool IsValid =>
			F.UriF.IsHttps(Webhook);
	}
}
