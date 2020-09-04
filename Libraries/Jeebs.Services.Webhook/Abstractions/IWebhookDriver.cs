using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Jeebs.Config;
using Jeebs.Services.Webhook.Models;

namespace Jeebs.Services.Webhook
{
	/// <summary>
	/// Messenger service
	/// </summary>
	/// <typeparam name="TConfig">Service configuration</typeparam>
	/// <typeparam name="TMessage">Message type</typeparam>
	public interface IWebhookDriver<TConfig, TMessage>
		where TConfig : ServiceConfig
		where TMessage : notnull
	{
		/// <summary>
		/// Send a message using default options
		/// <para>Separate from <see cref="Send(string, MessageLevel)"/> so webhooks can also be used as <see cref="INotificationListener"/></para>
		/// </summary>
		/// <param name="message">Message content</param>
		void Send(string message);

		/// <summary>
		/// Send a message using default options
		/// <para>Separate from <see cref="Send(string)"/> so webhooks can also be used as <see cref="INotificationListener"/></para>
		/// </summary>
		/// <param name="message">Message content</param>
		/// <param name="level">Message level</param>
		void Send(string message, MessageLevel level);

		/// <summary>
		/// Send an IMsg
		/// </summary>
		/// <param name="msg"></param>
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
