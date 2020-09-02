using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Jeebs.Config;
using Jeebs.Services.Webhook;
using Jeebs.Services.Webhook.Models;

namespace Jeebs.Services.Drivers.Webhook.Slack
{
	/// <summary>
	/// Slack service
	/// </summary>
	public abstract class SlackWebhookDriver : WebhookDriver<SlackConfig>
	{
		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="name">Service name</param>
		/// <param name="args">SlackWebhookDriverArgs</param>
		protected SlackWebhookDriver(string name, SlackWebhookDriverArgs args) : base(name, args) { }

		/// <inheritdoc/>
		public override void Send(Message message)
		{
			// Build request message
			var request = new HttpRequestMessage(HttpMethod.Post, ServiceConfig.Webhook);

			// Create event and add to the message
			var m = new SlackMessage(JeebsConfig, message.Content, message.Level);
			request.Content = new JsonHttpContent(m);

			// Send request
			Send(request);
		}
	}
}
