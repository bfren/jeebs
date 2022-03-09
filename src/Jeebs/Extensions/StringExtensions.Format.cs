// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Functions;

namespace Jeebs.Extensions;

public static partial class StringExtensions
{
	/// <inheritdoc cref="StringF.Format{T}(string, T)"/>
	public static string Format<T>(this string @this, T source) =>
		Modify(@this, () => StringF.Format(@this, source));
}
