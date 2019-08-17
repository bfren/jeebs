namespace Jeebs.WordPress.Entities
{
	/// <summary>
	/// PostMeta entity
	/// </summary>
	public class WpPostMetaEntity
	{
		/// <summary>
		/// PostMetaId
		/// </summary>
		public int PostMetaId { get; set; }

		/// <summary>
		/// PostId
		/// </summary>
		public int PostId { get; set; }

		/// <summary>
		/// Key
		/// </summary>
		public string Key { get; set; }

		/// <summary>
		/// Value
		/// </summary>
		public string Value { get; set; }
	}
}
