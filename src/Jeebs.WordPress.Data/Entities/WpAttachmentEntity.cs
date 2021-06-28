// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using static F.JsonF;

namespace Jeebs.WordPress.Data.Entities
{
	/// <summary>
	/// Attachment entity
	/// </summary>
	public abstract record WpAttachmentEntity : WpPostEntity, IAttachment
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
