using Jeebs.Data;

namespace Jeebs.WordPress.Entities
{
	/// <summary>
	/// UserMeta entity
	/// </summary>
	public abstract class WpUserMetaEntity : IEntity
	{
		/// <summary>
		/// Id
		/// </summary>
		[Ignore]
		public long Id
		{
			get => UserMetaId;
			set => UserMetaId = value;
		}

		/// <summary>
		/// UserMetaId
		/// </summary>
		[Id]
		public long UserMetaId { get; set; }

		/// <summary>
		/// UserId
		/// </summary>
		public long UserId { get; set; }

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
