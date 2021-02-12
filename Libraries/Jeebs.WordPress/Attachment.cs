using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.WordPress.Entities
{
	/// <summary>
	/// Post Attachment
	/// </summary>
	public abstract record Attachment : WpPostEntity
	{
		/// <summary>
		/// MetaDictionary
		/// </summary>
		public MetaDictionary Meta { get; set; } = new MetaDictionary();

		/// <summary>
		/// UrlPath
		/// </summary>
		public string UrlPath { get; set; } = string.Empty;

		/// <summary>
		/// Deserialise <see cref="info"/> and return as JSON
		/// </summary>
		public string Info
		{
			get =>
				F.JsonF.Serialise(F.PhpF.Deserialise(info));

			set =>
				info = value;
		}

		/// <summary>
		/// PHP serialised info
		/// </summary>
		private string info = string.Empty;
	}
}
