// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.uk/2013

namespace Jeebs.Config
{
	/// <summary>
	/// Webhook Service configuration
	/// </summary>
	public abstract record WebhookServiceConfig : IServiceConfig
	{
		/// <summary>
		/// Webhook URI
		/// </summary>
		public virtual string Webhook { get; init; } = string.Empty;

		/// <inheritdoc/>
		public abstract bool IsValid { get; }
	}
}
