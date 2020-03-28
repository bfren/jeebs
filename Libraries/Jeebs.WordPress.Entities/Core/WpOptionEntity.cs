using Jeebs.Data;

namespace Jeebs.WordPress.Entities
{
	/// <summary>
	/// Option entity
	/// </summary>
	public abstract class WpOptionEntity : IEntity
	{
		/// <summary>
		/// Id
		/// </summary>
		[Ignore]
		public long Id { get => OptionId; set => OptionId = value; }
		/// <summary>
		/// OptionId
		/// </summary>
		[Id]
		public long OptionId { get; set; }

		/// <summary>
		/// Key
		/// </summary>
		public string Key { get; set; } = string.Empty;

		/// <summary>
		/// Value
		/// </summary>
		public string Value { get; set; } = string.Empty;

		/// <summary>
		/// IsAutoloaded
		/// </summary>
		public bool IsAutoloaded { get; set; }
	}
}
