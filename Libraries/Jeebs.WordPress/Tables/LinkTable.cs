using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Data.Mapping;

namespace Jeebs.WordPress.Tables
{
	/// <summary>
	/// Link Table
	/// </summary>
	public sealed class LinkTable : Table
	{
		/// <summary>
		/// LinkId
		/// </summary>
		public string LinkId { get; } = "link_id";

		/// <summary>
		/// Url
		/// </summary>
		public string Url { get; } = "link_url";

		/// <summary>
		/// Title
		/// </summary>
		public string Title { get; } = "link_name";

		/// <summary>
		/// Image
		/// </summary>
		public string Image { get; } = "link_image";

		/// <summary>
		/// Target
		/// </summary>
		public string Target { get; } = "link_target";

		/// <summary>
		/// CategoryId
		/// </summary>
		public string CategoryId { get; } = "link_category";

		/// <summary>
		/// Description
		/// </summary>
		public string Description { get; } = "link_description";

		/// <summary>
		/// Visible
		/// </summary>
		public string Visible { get; } = "link_visible";

		/// <summary>
		/// OwnerId
		/// </summary>
		public string OwnerId { get; } = "link_owner";

		/// <summary>
		/// Rating
		/// </summary>
		public string Rating { get; } = "link_rating";

		/// <summary>
		/// LastUpdatedOn
		/// </summary>
		public string LastUpdatedOn { get; } = "link_updated";

		/// <summary>
		/// Rel
		/// </summary>
		public string Rel { get; } = "link_rel";

		/// <summary>
		/// Notes
		/// </summary>
		public string Notes { get; } = "link_notes";

		/// <summary>
		/// Rss
		/// </summary>
		public string Rss { get; } = "link_rss";

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="prefix">Table prefix</param>
		public LinkTable(string prefix) : base($"{prefix}links") { }
	}
}
