using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Config
{
	/// <summary>
	/// Webhook Service configuration
	/// </summary>
	public abstract record WebhookServiceConfig : ServiceConfig
	{
		/// <summary>
		/// Webhook URI
		/// </summary>
		public virtual string Webhook { get; set; } = string.Empty;
	}
}
