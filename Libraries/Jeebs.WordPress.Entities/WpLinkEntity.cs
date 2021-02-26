using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Data;
using Jeebs.Data.Mapping;

namespace Jeebs.WordPress.Entities
{
	/// <summary>
	/// Link entity
	/// </summary>
	public abstract record WpLinkEntity : IEntity, IEntity<long>
	{
		/// <summary>
		/// Id
		/// </summary>
		[Ignore]
		long IEntity.Id =>
			Id.Value;

		/// <summary>
		/// Id
		/// </summary>
		[Ignore]
		public IStrongId<long> Id
		{
			get =>
				new WpLinkId(LinkId);

			init =>
				LinkId = value.Value;
		}

		/// <summary>
		/// LinkId
		/// </summary>
		[Id]
		public long LinkId { get; set; }

		/// <summary>
		/// Url
		/// </summary>
		public string Url { get; set; } = string.Empty;

		/// <summary>
		/// Title
		/// </summary>
		public string Title { get; set; } = string.Empty;

		/// <summary>
		/// Image
		/// </summary>
		public string Image { get; set; } = string.Empty;

		/// <summary>
		/// Target
		/// </summary>
		public string Target { get; set; } = string.Empty;

		/// <summary>
		/// CategoryId
		/// </summary>
		public long CategoryId { get; set; }

		/// <summary>
		/// Description
		/// </summary>
		public string Description { get; set; } = string.Empty;

		/// <summary>
		/// Visible
		/// </summary>
		public bool Visible { get; set; }

		/// <summary>
		/// OwnerId
		/// </summary>
		public long OwnerId { get; set; }

		/// <summary>
		/// Rating
		/// </summary>
		public long Rating { get; set; }

		/// <summary>
		/// LastUpdatedOn
		/// </summary>
		public DateTime LastUpdatedOn { get; set; }

		/// <summary>
		/// Rel
		/// </summary>
		public string Rel { get; set; } = string.Empty;

		/// <summary>
		/// Notes
		/// </summary>
		public string Notes { get; set; } = string.Empty;

		/// <summary>
		/// Rss
		/// </summary>
		public string Rss { get; set; } = string.Empty;
	}
}
