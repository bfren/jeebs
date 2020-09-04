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
	public abstract class RocketChatWebhookDriver : WebhookDriver<RocketChatConfig, RocketChatMessage>
	{
		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="name">Service name</param>
		/// <param name="args">RocketChatWebhookDriverArgs</param>
		protected RocketChatWebhookDriver(string name, RocketChatWebhookDriverArgs args) : base(name, args) { }

		/// <inheritdoc/>
		public override void Send(Message message)
			=> Send(new RocketChatMessage(JeebsConfig, message.Content, message.Level));
	}
}
