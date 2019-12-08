namespace Jeebs.WordPress.Entities
{
	/// <summary>
	/// CommentMeta entity
	/// </summary>
	public abstract class WpCommentMetaEntity
	{

		/// <summary>
		/// CommentMetaId
		/// </summary>
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
