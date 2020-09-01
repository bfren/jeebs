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
	public interface IWebhookDriver<TConfig>
		where TConfig : ServiceConfig
	{
		/// <summary>
		/// Send a message using default options
		/// </summary>
		/// <param name="message">Message content</param>
		/// <param name="level">[Optional] Message level</param>
		void Send(string message, MessageLevel level = MessageLevel.Information);

		/// <summary>
		/// Send a message
		/// </summary>
		/// <param name="message"></param>
		void Send(Message message);
	}
}
