using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Jeebs;
using Jeebs.Config;
using Jeebs.Services.Drivers.Webhook.Seq;
using Microsoft.Extensions.Options;

namespace AppConsole
{
	public class Seq : SeqWebhookDriver
	{
		public Seq(SeqWebhookDriverArgs args) : base("wrong", args) { }
	}
}
