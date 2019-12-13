using Jeebs.Data;

namespace Jeebs.WordPress.Entities
{
	/// <summary>
	/// CommentMeta entity
	/// </summary>
	public abstract class WpCommentMetaEntity : IEntity
	{
		/// <summary>
		/// Id
		/// </summary>
		[Ignore]
		public int Id { get => CommentMetaId; set => CommentMetaId = value; }

		/// <summary>
		/// CommentMetaId
		/// </summary>
		[Id]
		public int CommentMetaId { get; set; }

		/// <summary>
		/// CommentId
		/// </summary>
		public int CommentId { get; set; }

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
