using Jeebs.Data;

namespace Jeebs.WordPress.Entities
{
	/// <summary>
	/// Term entity
	/// </summary>
	public abstract class WpTermEntity
	{
		/// <summary>
		/// TermId
		/// </summary>
		[Id]
		public int TermId { get; set; }

		/// <summary>
		/// Title
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// Slug
		/// </summary>
		public string Slug { get; set; }

		/// <summary>
		/// Group
		/// </summary>
		public int Group { get; set; }

		/// <summary>
		/// Create object
		/// </summary>
		protected WpTermEntity()
		{
			Title = string.Empty;
			Slug = string.Empty;
		}
	}
}
