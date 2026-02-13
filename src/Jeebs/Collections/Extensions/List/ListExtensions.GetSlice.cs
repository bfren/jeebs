// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;

namespace Jeebs.Collections;

/// <summary>
/// <see cref="List{T}"/> extension methods.
/// </summary>
public static partial class ListExtensions
{
	/// <summary>
	/// Get a slice of values from a list.
	/// </summary>
	/// <typeparam name="T">List item type.</typeparam>
	/// <param name="this">List.</param>
	/// <param name="range">Range of slice to return (exclusive of end element).</param>
	/// <returns>Slice of values defined by <paramref name="range"/>.</returns>
	public static List<T> GetSlice<T>(this List<T> @this, Range range)
	{
		var (start, length) = range.GetOffsetAndLength(@this.Count);
		return @this.GetRange(start, length);
	}
}
