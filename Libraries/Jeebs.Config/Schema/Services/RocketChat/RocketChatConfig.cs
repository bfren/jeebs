// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

namespace Jeebs.Config
{
	/// <summary>
	/// Rocket.Chat configuration
	/// </summary>
	public record RocketChatConfig : WebhookServiceConfig
	{
		/// <inheritdoc/>
		public override bool IsValid =>
			JeebsF.UriF.IsHttps(Webhook);
	}
}
