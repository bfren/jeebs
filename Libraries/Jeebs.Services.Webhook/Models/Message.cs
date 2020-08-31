using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Services.Webhook.Models
{
	/// <summary>
	/// Message model
	/// </summary>
	public sealed class Message
	{
		/// <summary>
		/// Message content
		/// </summary>
		public string Content { get; set; } = string.Empty;

		/// <summary>
		/// Message level
		/// </summary>
		public MessageLevel Level { get; set; }

		/// <summary>
		/// Additional fields to send
		/// </summary>
		public Dictionary<string, object> Fields { get; set; } = new Dictionary<string, object>();
	}
}
