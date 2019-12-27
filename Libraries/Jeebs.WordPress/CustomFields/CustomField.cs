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
	public abstract class CustomField<T>
	{
		/// <summary>
		/// Meta key (for post_meta table)
		/// </summary>
		public string Key { get; }

		/// <summary>
		/// Custom Field value
		/// </summary>
		public T Value { get; internal set; }

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="key">Meta key (for post_meta table)</param>
		/// <param name="defaultValue">Default value</param>
		protected CustomField(in string key, in T defaultValue)
		{
			Key = key;
			Value = defaultValue;
		}

		/// <summary>
		/// Return the post_meta key instead of the class name
		/// </summary>
		public override string ToString() => Key;
	}
}
