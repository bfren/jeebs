using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Services.Drivers.Webhook.RocketChat;

namespace AppConsole
{
	class RocketChat : RocketChatWebhookDriver
	{
		public RocketChat(RocketChatWebhookDriverArgs args) : base("bcg.xyz", args) { }
	}
}
