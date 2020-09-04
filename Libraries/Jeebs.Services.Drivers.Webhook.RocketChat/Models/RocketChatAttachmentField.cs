using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Services.Drivers.Webhook.RocketChat.Models
{
	/// <summary>
	/// RocketChat Attachment Field
	/// </summary>
	public sealed class RocketChatAttachmentField
	{
		/// <summary>
		/// Whether or not this is a short field
		/// </summary>
		public bool Short { get; set; }

		/// <summary>
		/// Field title
		/// </summary>
		public string Title { get; }

		/// <summary>
		/// Field value
		/// </summary>
		public string Value { get; }

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="title">Field title</param>
		/// <param name="value">Field value</param>
		public RocketChatAttachmentField(string title, string value)
			=> (Title, Value) = (title, value);
	}
}
