using Jeebs.Data;

namespace Jeebs.WordPress.Entities
{
	/// <summary>
	/// PostMeta entity
	/// </summary>
	public abstract class WpPostMetaEntity : IEntity
	{
		/// <summary>
		/// Id
		/// </summary>
		[Ignore]
		public long Id { get => PostMetaId; set => PostMetaId = value; }

		/// <summary>
		/// PostMetaId
		/// </summary>
		[Id]
		public long PostMetaId { get; set; }

		/// <summary>
		/// PostId
		/// </summary>
		public long PostId { get; set; }

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
