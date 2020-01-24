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
		public int Id { get => PostMetaId; set => PostMetaId = value; }

		/// <summary>
		/// PostMetaId
		/// </summary>
		[Id]
		public int PostMetaId { get; set; }

		/// <summary>
		/// PostId
		/// </summary>
		public int PostId { get; set; }

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
