namespace Jeebs.WordPress.Entities
{
	/// <summary>
	/// TermMeta entity
	/// </summary>
	public class WpTermMetaEntity
	{
		/// <summary>
		/// TermMetaId
		/// </summary>
		public int TermMetaId { get; set; }

		/// <summary>
		/// TermId
		/// </summary>
		public int TermId { get; set; }

		/// <summary>
		/// Key
		/// </summary>
		public string? Key { get; set; }

		/// <summary>
		/// Value
		/// </summary>
		public string? Value { get; set; }
	}
}
