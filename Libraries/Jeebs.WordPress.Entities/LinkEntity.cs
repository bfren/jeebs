using System;

namespace Jeebs.WordPress.Entities
{
	/// <summary>
	/// Link entity
	/// </summary>
	public abstract class WpLinkEntity
	{
		/// <summary>
		/// LinkId
		/// </summary>
		public int LinkId { get; set; }

		/// <summary>
		/// Url
		/// </summary>
		public string Url { get; set; }

		/// <summary>
		/// Title
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// Image
		/// </summary>
		public string Image { get; set; }

		/// <summary>
		/// Target
		/// </summary>
		public string Target { get; set; }

		/// <summary>
		/// CategoryId
		/// </summary>
		public int CategoryId { get; set; }

		/// <summary>
		/// Description
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Visible
		/// </summary>
		public bool Visible { get; set; }

		/// <summary>
		/// OwnerId
		/// </summary>
		public int OwnerId { get; set; }

		/// <summary>
		/// Rating
		/// </summary>
		public int Rating { get; set; }

		/// <summary>
		/// LastUpdatedOn
		/// </summary>
		public DateTime LastUpdatedOn { get; set; }

		/// <summary>
		/// Rel
		/// </summary>
		public string Rel { get; set; }

		/// <summary>
		/// Notes
		/// </summary>
		public string Notes { get; set; }

		/// <summary>
		/// Rss
		/// </summary>
		public string Rss { get; set; }

		/// <summary>
		/// Create object
		/// </summary>
		public WpLinkEntity()
		{
			Url = string.Empty;
			Title = string.Empty;
			Image = string.Empty;
			Target = string.Empty;
			Description = string.Empty;
			Rel = string.Empty;
			Notes = string.Empty;
			Rss = string.Empty;
		}
	}
}
