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
		public int Id { get => UserMetaId; set => UserMetaId = value; }

		/// <summary>
		/// UserMetaId
		/// </summary>
		[Id]
		public int UserMetaId { get; set; }

		/// <summary>
		/// UserId
		/// </summary>
		public int UserId { get; set; }

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
