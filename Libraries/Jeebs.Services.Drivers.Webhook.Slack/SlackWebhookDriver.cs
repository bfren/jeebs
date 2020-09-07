using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Jeebs.Config;
using Jeebs.Services.Drivers.Webhook.Slack.Models;
using Jeebs.Services.Webhook;
using Jeebs.Services.Webhook.Models;

namespace Jeebs.Services.Drivers.Webhook.Slack
{
	/// <summary>
	/// Slack Webhook Driver
	/// </summary>
	public abstract class SlackWebhookDriver : WebhookDriver<SlackConfig, SlackMessage>
	{
		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="name">Service name</param>
		/// <param name="args">SlackWebhookDriverArgs</param>
		protected SlackWebhookDriver(string name, SlackWebhookDriverArgs args) : base(name, args) { }

		/// <inheritdoc/>
		public override void Send(Message message)
			=> Send(new SlackMessage(JeebsConfig, message.Content, message.Level));
	}
}
