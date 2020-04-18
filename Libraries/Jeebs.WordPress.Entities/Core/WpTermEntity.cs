using Jeebs.Data;

namespace Jeebs.WordPress.Entities
{
	/// <summary>
	/// Term entity
	/// </summary>
	public abstract class WpTermEntity : IEntity
	{
		/// <summary>
		/// Id
		/// </summary>
		[Ignore]
		public long Id { get => TermId; set => TermId = value; }

		/// <summary>
		/// TermId
		/// </summary>
		[Id]
		public long TermId { get; set; }

		/// <summary>
		/// Title
		/// </summary>
		public string Title { get; set; } = string.Empty;

		/// <summary>
		/// Slug
		/// </summary>
		public string Slug { get; set; } = string.Empty;

		/// <summary>
		/// Group
		/// </summary>
		public long Group { get; set; }
	}
}
