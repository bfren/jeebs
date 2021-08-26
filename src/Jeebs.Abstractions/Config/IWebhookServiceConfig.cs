// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs.Config
{
	/// <summary>
	/// Webhook service configuration interface
	/// </summary>
	public interface IWebhookServiceConfig : IServiceConfig
	{
		/// <summary>
		/// Webhook URI
		/// </summary>
		string Webhook { get; }
	}
}
