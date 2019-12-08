using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Apps.Config.Logging
{
	/// <summary>
	/// Slack Provider
	/// </summary>
	public sealed class SlackProvider : LoggingProvider
	{
		/// <summary>
		/// Web hook URI
		/// </summary>
		public string Webhook { get; set; }

		/// <summary>
		/// Whether or not this provider's configuraiton is valid
		/// </summary>
		/// <returns>True if the webhook is not empty</returns>
		public override bool IsValid() => !string.IsNullOrEmpty(Webhook);

		/// <summary>
		/// Create object
		/// </summary>
		public SlackProvider()
		{
			Webhook = string.Empty;
		}
	}
}
