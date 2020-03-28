using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Util;

namespace Jeebs.WordPress.Entities
{
	/// <summary>
	/// Post Attachment
	/// </summary>
	public abstract class WpPostAttachmentEntity : WpPostEntity
	{
		/// <summary>
		/// UrlPath
		/// </summary>
		public string UrlPath { get; set; } = string.Empty;

		/// <summary>
		/// Deserialise info and return as JSON
		/// </summary>
		public string Info
		{
			get => Json.Serialise(new PhpSerialiser().Deserialise(info));
			set => info = value;
		}

		/// <summary>
		/// Serialised info
		/// </summary>
		private string info = string.Empty;
	}
}
