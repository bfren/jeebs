using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;
using Jeebs.Services.Drivers.Webhook.RocketChat;

namespace AppConsole
{
	internal class RocketChat : RocketChatWebhookDriver, INotificationListener
	{
		public RocketChat(RocketChatWebhookDriverArgs args) : base("bcg.xyz", args) { }
	}
}
