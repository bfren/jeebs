// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;

namespace Jeebs.Functions;

public static partial class UriF
{
	/// <summary>
	/// Returns true if <paramref name="input"/> is a valid HTTP URI.
	/// </summary>
	/// <param name="input">Input URI.</param>
	/// <param name="requireHttps">Set to false if you want to match HTTP URIs.</param>
	/// <returns>Whether or not <paramref name="input"/> is a valid HTTP URI.</returns>
	public static bool IsHttp(string input, bool requireHttps) =>
		!string.IsNullOrEmpty(input)
		&& Uri.TryCreate(input, UriKind.Absolute, out var uri)
		&& (uri.Scheme == Uri.UriSchemeHttps || (!requireHttps && uri.Scheme == Uri.UriSchemeHttp));
}
