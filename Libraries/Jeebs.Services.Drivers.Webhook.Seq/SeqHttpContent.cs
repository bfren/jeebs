using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Jeebs.Util;
using Newtonsoft.Json;

namespace Jeebs.Services.Drivers.Webhook.Seq
{
	/// <summary>
	/// HttpContent for Seq Event
	/// </summary>
	public sealed class SeqHttpContent : StringContent
	{
		/// <summary>
		/// Serialise <paramref name="e"/> as JSON, and add Serilog media type
		/// </summary>
		/// <param name="e">SeqEvent</param>
		public SeqHttpContent(SeqEvent e) : base(Json.Serialise(e), Encoding.UTF8, "application/vnd.serilog.clef") { }
	}
}
