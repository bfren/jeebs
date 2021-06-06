// Jeebs Test Applications
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Services.Drivers.Webhook.Seq;

namespace AppConsole
{
	internal class Seq : SeqWebhookDriver
	{
		public Seq(SeqWebhookDriverArgs args) : base("wrong", args) { }
	}
}
