using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Config.Logging
{
	/// <summary>
	/// Rocket.Chat Provider
	/// </summary>
	public sealed class SeqProvider : LoggingProvider
	{
		/// <summary>
		/// Seq Server URI
		/// </summary>
		public string Server { get; set; } = string.Empty;

		/// <summary>
		/// Seq Server API Key
		/// </summary>
		public string ApiKey { get; set; } = string.Empty;

		/// <inheritdoc/>
		public override bool IsValid()
			=> !string.IsNullOrEmpty(Server) && !string.IsNullOrEmpty(ApiKey);
	}
}
