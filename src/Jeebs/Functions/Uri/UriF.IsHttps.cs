// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Functions;

public static partial class UriF
{
	/// <inheritdoc cref="IsHttp(string, bool)"/>
	public static bool IsHttps(string input) =>
		IsHttp(input, true);
}
