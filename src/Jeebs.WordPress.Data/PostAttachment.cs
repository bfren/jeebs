// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.WordPress.Data.Entities;
using static F.JsonF;

namespace Jeebs.WordPress.Data
{
	/// <summary>
	/// Post Attachment
	/// </summary>
	public abstract record class PostAttachment : WpPostEntity, IPostAttachment
	{
		/// <inheritdoc/>
		public string Description
		{
			get =>
				Excerpt;

			init =>
				Excerpt = value;
		}

		/// <inheritdoc/>
		public MetaDictionary Meta { get; init; } = new();

		/// <inheritdoc/>
		public string UrlPath { get; init; } = string.Empty;

		/// <inheritdoc/>
		public string Info
		{
			get =>
				Serialise(
					F.PhpF.Deserialise(info)
				)
				.Unwrap(
					Empty
				);

			init =>
				info = value;
		}

		/// <summary>
		/// PHP serialised info
		/// </summary>
		private string info = string.Empty;

		/// <inheritdoc/>
		public string GetFilePath(string wpUploadsPath) =>
			wpUploadsPath.EndWith('/') + UrlPath.TrimStart('/');
	}
}
