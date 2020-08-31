using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Jeebs.Services.Drivers.Webhook.Seq.Models;
using Jeebs.Util;
using Newtonsoft.Json;

namespace Jeebs.Services.Drivers.Webhook.Seq
{
	public sealed class SeqHttpContent : StringContent
	{
		public SeqHttpContent(Event e) : base(Json.Serialise(e), Encoding.UTF8, "application/vnd.serilog.clef") { }
	}
}
