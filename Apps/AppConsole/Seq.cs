// Jeebs Test Applications
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Services.Drivers.Webhook.Seq;

namespace AppConsole
{
	internal class Seq : SeqWebhookDriver
	{
		public Seq(SeqWebhookDriverArgs args) : base("wrong", args) { }
	}
}
