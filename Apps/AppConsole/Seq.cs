using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Jeebs.Services.Drivers.Webhook.Seq;

namespace AppConsole
{
	internal class Seq : SeqWebhookDriver
	{
		public Seq(SeqWebhookDriverArgs args) : base("wrong", args) { }
	}
}
