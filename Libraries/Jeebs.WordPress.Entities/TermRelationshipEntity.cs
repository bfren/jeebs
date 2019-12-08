namespace Jeebs.WordPress.Entities
{
	/// <summary>
	/// TermRelationship entity
	/// </summary>
	public abstract class WpTermRelationshipEntity
	{
		/// <summary>
		/// PostId
		/// </summary>
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
