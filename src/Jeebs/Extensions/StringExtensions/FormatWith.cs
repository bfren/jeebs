// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs;

public static partial class StringExtensions
{
	/// <inheritdoc cref="F.MsgF.Format{T}(string, T)"/>
	public static string FormatWith<T>(this string @this, T source) =>
		Modify(@this, () => F.MsgF.Format(@this, source));
}
