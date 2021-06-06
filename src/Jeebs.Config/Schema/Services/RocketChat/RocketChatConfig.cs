// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs.Config
{
	/// <summary>
	/// Rocket.Chat configuration
	/// </summary>
	public record RocketChatConfig : WebhookServiceConfig
	{
		/// <inheritdoc/>
		public override bool IsValid =>
			F.UriF.IsHttps(Webhook);
	}
}
