using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Config
{
	/// <summary>
	/// Rocket.Chat configuration
	/// </summary>
	public class RocketChatConfig : WebhookServiceConfig
	{
		/// <inheritdoc/>
		public override bool IsValid()
			=> !string.IsNullOrEmpty(Webhook);
	}
}
