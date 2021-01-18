using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Config
{
	/// <summary>
	/// Slack configuration
	/// </summary>
	public class SlackConfig : WebhookServiceConfig
	{
		/// <summary>
		/// Whether or not to add attachments to a message (error type and timestamp)
		/// </summary>
		public bool ShowAttachments { get; set; }

		/// <inheritdoc/>
		public override bool IsValid
			=> F.UriF.IsHttps(Webhook);
	}
}
