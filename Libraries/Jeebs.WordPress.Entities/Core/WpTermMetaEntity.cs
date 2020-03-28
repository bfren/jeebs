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
		public long Id { get => TermMetaId; set => TermMetaId = value; }

		/// <summary>
		/// TermMetaId
		/// </summary>
		[Id]
		public long TermMetaId { get; set; }

		/// <summary>
		/// TermId
		/// </summary>
		public long TermId { get; set; }

		/// <summary>
		/// Key
		/// </summary>
		public string Key { get; set; } = string.Empty;

		/// <summary>
		/// Value
		/// </summary>
		public string Value { get; set; } = string.Empty;
	}
}
