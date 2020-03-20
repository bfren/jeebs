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
		public long Id { get; set; }

		/// <summary>
		/// PostId
		/// </summary>
		[Id]
		public long PostId { get; set; }

		/// <summary>
		/// TermTaxonomyId
		/// </summary>
		public long TermTaxonomyId { get; set; }

		/// <summary>
		/// SortOrder
		/// </summary>
		public int SortOrder { get; set; }
	}
}
