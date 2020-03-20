using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs.WordPress
{
	/// <summary>
	/// Represents a CustomField, which are stored in the post_meta table
	/// </summary>
	/// <typeparam name="T">Value type</typeparam>
	public abstract class CustomField<T> : ICustomField<T>
	{
		/// <summary>
		/// Meta key (for post_meta table)
		/// </summary>
		public string Key { get; }

		/// <summary>
		/// Custom Field value
		/// </summary>
		public abstract T Val { get; }

		/// <summary>
		/// String representation of the value - normally retrieved from the database
		/// </summary>
		protected readonly string value = string.Empty;

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="key">Meta key (for post_meta table)</param>
		protected CustomField(string key) => Key = key;

		/// <summary>
		/// Return the value, or post_meta key (instead of the class name)
		/// </summary>
		public override string ToString() => Val?.ToString() ?? Key;
	}
}
