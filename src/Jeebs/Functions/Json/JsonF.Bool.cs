// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Functions;

public static partial class JsonF
{
	/// <summary>
	/// Return lower-case boolean string.
	/// </summary>
	/// <param name="value">Boolean value.</param>
	/// <returns>JSON boolean string.</returns>
	public static string Bool(bool value) =>
		value switch
		{
			true =>
				"true",

			false =>
				"false"
		};
}
