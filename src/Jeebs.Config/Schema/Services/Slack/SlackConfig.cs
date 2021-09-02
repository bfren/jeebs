// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Config
{
	/// <summary>
	/// Slack configuration
	/// </summary>
	public sealed record class SlackConfig : IWebhookServiceConfig
	{
		/// <inheritdoc/>
		public string Webhook { get; init; } = string.Empty;

		/// <summary>
		/// Whether or not to add attachments to a message (error type and timestamp)
		/// </summary>
		public bool ShowAttachments { get; init; }

		/// <inheritdoc/>
		public bool IsValid =>
			F.UriF.IsHttps(Webhook);
	}
}
