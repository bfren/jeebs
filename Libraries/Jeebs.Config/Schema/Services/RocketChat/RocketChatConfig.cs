using System;
using System.Collections.Generic;
using System.Text;

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
