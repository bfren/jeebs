// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using Jeebs.Data.Entities;

namespace Jeebs.WordPress.Data.Entities
{
	/// <summary>
	/// Link entity
	/// </summary>
	public abstract record WpLinkEntity : IWithId<WpLinkId>
	{
		/// <summary>
		/// Id
		/// </summary>
		[Ignore]
		public WpLinkId Id
		{
			get =>
				new(LinkId);

			init =>
				LinkId = value.Value;
		}

		/// <summary>
		/// LinkId
		/// </summary>
		[Id]
		public long LinkId { get; init; }

		/// <summary>
		/// Url
		/// </summary>
		public string Url { get; init; } = string.Empty;

		/// <summary>
		/// Title
		/// </summary>
		public string Title { get; init; } = string.Empty;

		/// <summary>
		/// Image
		/// </summary>
		public string Image { get; init; } = string.Empty;

		/// <summary>
		/// Target
		/// </summary>
		public string Target { get; init; } = string.Empty;

		/// <summary>
		/// CategoryId
		/// </summary>
		public long CategoryId { get; init; }

		/// <summary>
		/// Description
		/// </summary>
		public string Description { get; init; } = string.Empty;

		/// <summary>
		/// Visible
		/// </summary>
		public bool Visible { get; init; }

		/// <summary>
		/// OwnerId
		/// </summary>
		public long OwnerId { get; init; }

		/// <summary>
		/// Rating
		/// </summary>
		public long Rating { get; init; }

		/// <summary>
		/// LastUpdatedOn
		/// </summary>
		public DateTime LastUpdatedOn { get; init; }

		/// <summary>
		/// Rel
		/// </summary>
		public string Rel { get; init; } = string.Empty;

		/// <summary>
		/// Notes
		/// </summary>
		public string Notes { get; init; } = string.Empty;

		/// <summary>
		/// Rss
		/// </summary>
		public string Rss { get; init; } = string.Empty;
	}
}
