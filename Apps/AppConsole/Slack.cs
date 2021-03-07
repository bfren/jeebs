// Jeebs Test Applications
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs;
using Jeebs.Services.Drivers.Webhook.Slack;

namespace AppConsole
{
	internal class Slack : SlackWebhookDriver, INotificationListener
	{
		public Slack(SlackWebhookDriverArgs args) : base("bcgdesign", args) { }
	}
}
