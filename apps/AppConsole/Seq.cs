// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Services.Drivers.Webhook.Seq;

namespace AppConsole;

internal sealed class Seq : SeqWebhookDriver
{
	public Seq(SeqWebhookDriverArgs args) : base("wrong", args) { }
}
