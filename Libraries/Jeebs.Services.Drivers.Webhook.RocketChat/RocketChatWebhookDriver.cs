using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Jeebs.Config;
using Jeebs.Services.Drivers.Webhook.RocketChat.Models;
using Jeebs.Services.Webhook;
using Jeebs.Services.Webhook.Models;

namespace Jeebs.Services.Drivers.Webhook.RocketChat
{
	/// <summary>
	/// Slack service
	/// </summary>
	public abstract class RocketChatWebhookDriver : WebhookDriver<RocketChatConfig>
	{
		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="name">Service name</param>
		/// <param name="args">RocketChatWebhookDriverArgs</param>
		protected RocketChatWebhookDriver(string name, RocketChatWebhookDriverArgs args) : base(name, args) { }

		/// <inheritdoc/>
		public override void Send(Message message)
		{
			// Build request message
			var request = new HttpRequestMessage(HttpMethod.Post, ServiceConfig.Webhook);

			// Create event and add to the message
			var m = new RocketChatMessage(JeebsConfig, message.Content, message.Level);
			request.Content = new JsonHttpContent(m);

			// Send request
			Send(request);
		}
	}
}
