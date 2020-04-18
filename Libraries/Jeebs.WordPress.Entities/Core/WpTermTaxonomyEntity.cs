using Jeebs.Data;
using Jeebs.WordPress.Enums;

namespace Jeebs.WordPress.Entities
{
	/// <summary>
	/// TermTaxonomy entity
	/// </summary>
	public abstract class WpTermTaxonomyEntity : IEntity
	{
		/// <summary>
		/// Id
		/// </summary>
		[Ignore]
		public long Id { get => TermTaxonomyId; set => TermTaxonomyId = value; }

		/// <summary>
		/// TermTaxonomyId
		/// </summary>
		[Id]
		public long TermTaxonomyId { get; set; }

		/// <summary>
		/// TermId
		/// </summary>
		public long TermId { get; set; }

		/// <summary>
		/// Taxonomy
		/// </summary>
		public Taxonomy Taxonomy { get; set; } = Taxonomy.Blank;

		/// <summary>
		/// Description
		/// </summary>
		public string Description { get; set; } = string.Empty;

		/// <summary>
		/// ParentId
		/// </summary>
		public long ParentId { get; set; }

		/// <summary>
		/// Count
		/// </summary>
		public long Count { get; set; }
	}
}
