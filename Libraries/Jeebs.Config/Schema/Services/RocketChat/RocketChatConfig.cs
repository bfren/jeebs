using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Config
{
	/// <summary>
	/// Rocket.Chat configuration
	/// </summary>
	public class RocketChatConfig
	{
		/// <summary>
		/// Web hook URI
		/// </summary>
		public string Webhook { get; set; } = string.Empty;
	}
}
