// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.uk/2013

using Jeebs.Config;
using Jeebs.Services.Drivers.Webhook.RocketChat.Models;
using Jeebs.Services.Webhook;

namespace Jeebs.Services.Drivers.Webhook.RocketChat
{
	/// <summary>
	/// RocketChat Webhook Driver
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
		public override void Send(IWebhookMessage message) =>
			Send(new RocketChatMessage(JeebsConfig, message.Content, message.Level));
	}
}
