// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs;

/// <summary>
/// Extension methods for <see cref="System.Array"/> objects.
/// </summary>
public static class ArrayExtensions
{
	/// <summary>
	/// Extend an array with additional items
	/// </summary>
	/// <typeparam name="T">Type</typeparam>
	/// <param name="this">Original array</param>
	/// <param name="additionalItems">Additional items</param>
	public static T[] ExtendWith<T>(this T[] @this, params T[] additionalItems)
	{
		if (additionalItems is null || additionalItems.Length == 0)
		{
			return @this;
		}

		return [.. @this, .. additionalItems];
	}
}
