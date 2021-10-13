// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace F;

/// <summary>
/// String functions
/// </summary>
public static class StringF
{
	/// <summary>
	/// If <paramref name="obj"/> is not null, use string.Format() -
	/// otherwise, return <paramref name="ifNull"/>
	/// </summary>
	/// <typeparam name="T">Object type</typeparam>
	/// <param name="format">Format string</param>
	/// <param name="obj">Object (nullable)</param>
	/// <param name="ifNull">Value to return if null</param>
	public static string? Format<T>(string format, T obj, string? ifNull) =>
		obj switch
		{
			T t =>
				string.Format(format, t),

			_ =>
				ifNull
		};

	/// <summary>
	/// If <paramref name="obj"/> is not null, use string.Format() - otherwise returns null
	/// </summary>
	/// <typeparam name="T">Object type</typeparam>
	/// <param name="format">Format string</param>
	/// <param name="obj">Object (nullable)</param>
	public static string? Format<T>(string format, T obj) =>
		Format(format, obj, null);
}
