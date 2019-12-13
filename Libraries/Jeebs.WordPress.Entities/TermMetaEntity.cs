using Jeebs.Data;

namespace Jeebs.WordPress.Entities
{
	/// <summary>
	/// TermMeta entity
	/// </summary>
	public abstract class WpTermMetaEntity : IEntity
	{
		/// <summary>
		/// Id
		/// </summary>
		[Ignore]
		public int Id { get => TermMetaId; set => TermMetaId = value; }

		/// <summary>
		/// TermMetaId
		/// </summary>
		[Id]
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
