using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Jeebs.Config;
using Jeebs.Services.Webhook.Models;

namespace Jeebs.Services.Webhook
{
	/// <summary>
	/// Webhook Driver
	/// </summary>
	/// <typeparam name="TConfig">Service configuration</typeparam>
	/// <typeparam name="TMessage">Message type</typeparam>
	public interface IWebhookDriver<TConfig, TMessage>
		where TConfig : ServiceConfig
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
		void Send(Message message);

		/// <summary>
		/// Send a message
		/// </summary>
		/// <param name="message"></param>
		void Send(TMessage message);
	}
}
