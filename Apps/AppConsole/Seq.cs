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
	public class Seq : SeqWebhookService
	{
		public Seq(ILog log, IOptions<JeebsConfig> config, IHttpClientFactory factory) : base("bcg-home", log, config.Value, factory) { }
	}
}
