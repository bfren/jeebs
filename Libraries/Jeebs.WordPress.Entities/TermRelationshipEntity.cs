using Jeebs.Data;

namespace Jeebs.WordPress.Entities
{
	/// <summary>
	/// TermRelationship entity
	/// </summary>
	public abstract class WpTermRelationshipEntity : IEntity
	{
		/// <summary>
		/// Id
		/// </summary>
		[Ignore]
		public int Id { get; set; }

		/// <summary>
		/// PostId
		/// </summary>
		[Id]
		public int PostId { get; set; }

		/// <summary>
		/// TermTaxonomyId
		/// </summary>
		public int TermTaxonomyId { get; set; }

		/// <summary>
		/// SortOrder
		/// </summary>
		public int SortOrder { get; set; }
	}
}
