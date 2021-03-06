// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

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
