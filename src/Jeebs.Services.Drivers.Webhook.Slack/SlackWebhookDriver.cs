// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.uk/2013

using Jeebs.Config;
using Jeebs.Services.Drivers.Webhook.Slack.Models;
using Jeebs.Services.Webhook;

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
		public override void Send(IWebhookMessage message) =>
			Send(new SlackMessage(JeebsConfig, message.Content, message.Level));
	}
}
