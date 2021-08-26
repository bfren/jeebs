// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs.Config
{
	/// <summary>
	/// Rocket.Chat configuration
	/// </summary>
	public sealed record class RocketChatConfig : IWebhookServiceConfig
	{
		/// <inheritdoc/>
		public string Webhook { get; init; } = string.Empty;

		/// <inheritdoc/>
		public bool IsValid =>
			F.UriF.IsHttps(Webhook);
	}
}
