namespace Jeebs.WordPress.Entities
{
	/// <summary>
	/// Option entity
	/// </summary>
	public abstract class WpOptionEntity
	{
		/// <summary>
		/// OptionId
		/// </summary>
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
