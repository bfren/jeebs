using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.WordPress
{
	/// <summary>
	/// Custom Field
	/// </summary>
	public interface ICustomField
	{
		/// <summary>
		/// Custom Field key
		/// </summary>
		string Key { get; }
	}

	/// <summary>
	/// Custom Field
	/// </summary>
	/// <typeparam name="T">Value type</typeparam>
	public interface ICustomField<T> : ICustomField
	{
		/// <summary>
		/// Custom Field Value
		/// </summary>
		T Val { get; }
	}
}
