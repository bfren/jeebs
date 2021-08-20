// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs.Config
{
	/// <summary>
	/// Slack configuration
	/// </summary>
	public readonly record struct SlackConfig : IWebhookServiceConfig
	{
		/// <inheritdoc/>
		public string Webhook { get; init; }

		/// <summary>
		/// Whether or not to add attachments to a message (error type and timestamp)
		/// </summary>
		public bool ShowAttachments { get; init; }

		/// <inheritdoc/>
		public bool IsValid =>
			F.UriF.IsHttps(Webhook);
	}
}
