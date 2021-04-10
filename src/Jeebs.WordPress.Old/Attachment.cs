// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using static F.JsonF;

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
				Serialise(F.PhpF.Deserialise(info)).Unwrap(Empty);

			set =>
				info = value;
		}

		/// <summary>
		/// PHP serialised info
		/// </summary>
		private string info = string.Empty;
	}
}
