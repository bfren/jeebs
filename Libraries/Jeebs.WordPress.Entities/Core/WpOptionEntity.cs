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
		public int Id { get => OptionId; set => OptionId = value; }
		/// <summary>
		/// OptionId
		/// </summary>
		[Id]
		public int OptionId { get; set; }

		/// <summary>
		/// Key
		/// </summary>
		public string Key { get; set; }

		/// <summary>
		/// Value
		/// </summary>
		public string Value { get; set; }

		/// <summary>
		/// IsAutoloaded
		/// </summary>
		public bool IsAutoloaded { get; set; }

		/// <summary>
		/// Create object
		/// </summary>
		protected WpOptionEntity()
		{
			Key = string.Empty;
			Value = string.Empty;
		}
	}
}
