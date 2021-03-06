// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

using System;

namespace F
{
	/// <summary>
	/// URI function
	/// </summary>
	public static class UriF
	{
		/// <summary>
		/// Returns true if <paramref name="input"/> is a valid HTTP URI
		/// </summary>
		/// <param name="input">Input URI</param>
		/// <param name="requireHttps">Set to false if you want to match HTTP URIs</param>
		public static bool IsHttp(string input, bool requireHttps) =>
			!string.IsNullOrEmpty(input)
			&& Uri.TryCreate(input, UriKind.Absolute, out Uri? uri)
			&& (uri.Scheme == Uri.UriSchemeHttps
				|| (!requireHttps && uri.Scheme == Uri.UriSchemeHttp)
			);

		/// <inheritdoc cref="IsHttp(string, bool)"/>
		public static bool IsHttps(string input) =>
			IsHttp(input, true);
	}
}
