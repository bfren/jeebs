// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.uk/2013

using Jeebs.Config;

namespace Jeebs.Services.Webhook
{
	/// <summary>
	/// Webhook Driver
	/// </summary>
	/// <typeparam name="TConfig">Service configuration</typeparam>
	/// <typeparam name="TMessage">Message type</typeparam>
	public interface IWebhookDriver<TConfig, TMessage>
		where TConfig : IServiceConfig
		where TMessage : notnull
	{
		/// <inheritdoc cref="INotifier.Send(string, NotificationLevel)"/>
		void Send(string message, NotificationLevel level = NotificationLevel.Information);

		/// <inheritdoc cref="INotifier.Send(IMsg)"/>
		void Send(IMsg msg);

		/// <summary>
		/// Send a message
		/// </summary>
		/// <param name="message"></param>
		void Send(IWebhookMessage message);

		/// <summary>
		/// Send a message
		/// </summary>
		/// <param name="message"></param>
		void Send(TMessage message);
	}
}
