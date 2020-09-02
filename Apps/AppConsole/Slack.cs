using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Services.Drivers.Webhook.Slack;

namespace AppConsole
{
	internal class Slack : SlackWebhookDriver
	{
		public Slack(SlackWebhookDriverArgs args) : base("ben@bcgdesign.com", args) { }
	}
}
