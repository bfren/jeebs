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
		public readonly string LinkId = "link_id";

		/// <summary>
		/// Url
		/// </summary>
		public readonly string Url = "link_url";

		/// <summary>
		/// Title
		/// </summary>
		public readonly string Title = "link_name";

		/// <summary>
		/// Image
		/// </summary>
		public readonly string Image = "link_image";

		/// <summary>
		/// Target
		/// </summary>
		public readonly string Target = "link_target";

		/// <summary>
		/// CategoryId
		/// </summary>
		public readonly string CategoryId = "link_category";

		/// <summary>
		/// Description
		/// </summary>
		public readonly string Description = "link_description";

		/// <summary>
		/// Visible
		/// </summary>
		public readonly string Visible = "link_visible";

		/// <summary>
		/// OwnerId
		/// </summary>
		public readonly string OwnerId = "link_owner";

		/// <summary>
		/// Rating
		/// </summary>
		public readonly string Rating = "link_rating";

		/// <summary>
		/// LastUpdatedOn
		/// </summary>
		public readonly string LastUpdatedOn = "link_updated";

		/// <summary>
		/// Rel
		/// </summary>
		public readonly string Rel = "link_rel";

		/// <summary>
		/// Notes
		/// </summary>
		public readonly string Notes = "link_notes";

		/// <summary>
		/// Rss
		/// </summary>
		public readonly string Rss = "link_rss";

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="prefix">Table prefix</param>
		public LinkTable(string prefix) : base($"{prefix}links") { }
	}
}
