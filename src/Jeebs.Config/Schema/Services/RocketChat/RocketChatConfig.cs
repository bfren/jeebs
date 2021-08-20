// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs.Config
{
	/// <summary>
	/// Rocket.Chat configuration
	/// </summary>
	public readonly record struct RocketChatConfig : IWebhookServiceConfig
	{
		/// <inheritdoc/>
		public string Webhook { get; init; }

		/// <inheritdoc/>
		public bool IsValid =>
			F.UriF.IsHttps(Webhook);
	}
}
