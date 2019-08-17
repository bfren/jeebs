namespace Jeebs.WordPress.Entities
{
	/// <summary>
	/// UserMeta entity
	/// </summary>
	public class WpUserMetaEntity
	{
		/// <summary>
		/// UserMetaId
		/// </summary>
		public int UserMetaId { get; set; }

		/// <summary>
		/// UserId
		/// </summary>
		public int UserId { get; set; }

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
