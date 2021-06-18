// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs.WordPress.Data
{
	/// <summary>
	/// Attachment entity
	/// </summary>
	public interface IAttachment
	{
		/// <summary>
		/// Attachment description
		/// </summary>
		string Description { get; init; }

		/// <summary>
		/// MetaDictionary
		/// </summary>
		MetaDictionary Meta { get; init; }

		/// <summary>
		/// UrlPath
		/// </summary>
		string UrlPath { get; init; }

		/// <summary>
		/// Additional information about the file (encoded as JSON)
		/// </summary>
		string Info { get; init; }
	}
}
