using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Config
{
	/// <summary>
	/// Seq configuration
	/// </summary>
	public record SeqConfig : WebhookServiceConfig
	{
		/// <inheritdoc/>
		public override string Webhook =>
			$"{Server}/api/events/raw?clef";

		/// <summary>
		/// Seq Server URI
		/// </summary>
		public string Server { get; set; } = string.Empty;

		/// <summary>
		/// Seq Server API Key
		/// </summary>
		public string ApiKey { get; set; } = string.Empty;

		/// <inheritdoc/>
		public override bool IsValid =>
			!string.IsNullOrEmpty(Server)
			&& !string.IsNullOrEmpty(ApiKey)
			&& F.UriF.IsHttps(Webhook);
	}
}
