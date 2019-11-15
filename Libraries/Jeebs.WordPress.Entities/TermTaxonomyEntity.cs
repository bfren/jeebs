using Jeebs.WordPress.Enums;

namespace Jeebs.WordPress.Entities
{
	/// <summary>
	/// TermTaxonomy entity
	/// </summary>
	public class WpTermTaxonomyEntity
	{
		/// <summary>
		/// TermTaxonomyId
		/// </summary>
		public int TermTaxonomyId { get; set; }

		/// <summary>
		/// TermId
		/// </summary>
		public int TermId { get; set; }

		/// <summary>
		/// Taxonomy
		/// </summary>
		public Taxonomy Taxonomy { get; set; }

		/// <summary>
		/// Description
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// ParentId
		/// </summary>
		public int ParentId { get; set; }

		/// <summary>
		/// Count
		/// </summary>
		public int Count { get; set; }

		/// <summary>
		/// Create object
		/// </summary>
		public WpTermTaxonomyEntity()
		{
			Taxonomy = Taxonomy.Blank;
			Description = string.Empty;
		}
	}
}
