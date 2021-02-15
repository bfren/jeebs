using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	/// <summary>
	/// Represents a strongly-typed ID
	/// </summary>
	public interface IStrongId
	{
		/// <summary>
		/// ID Value as string
		/// </summary>
		string ValueStr { get; }

		/// <summary>
		/// Returns true if the current value is the default (i.e. unset) value
		/// </summary>
		bool IsDefault { get; }
	}

	/// <summary>
	/// Represents a strongly-typed ID
	/// </summary>
	/// <typeparam name="T">ID value type</typeparam>
	public interface IStrongId<T> : IStrongId
		where T : notnull
	{
		/// <summary>
		/// ID Value
		/// </summary>
		T Value { get; init; }
	}
}
