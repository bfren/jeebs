// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

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
		public virtual string Webhook { get; init; } = string.Empty;
	}
}
