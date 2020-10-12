using System;
using System.Collections.Generic;
using System.Text;

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
		/// <param name="requireHttps">[Optional] Set to true if you want to require HTTPS URIs</param>
		public static bool IsHttp(string input, bool requireHttps = false)
			=> !string.IsNullOrEmpty(input)
			&& Uri.TryCreate(input, UriKind.Absolute, out Uri uri) switch
			{
				true => uri.Scheme == Uri.UriSchemeHttps || (!requireHttps && uri.Scheme == Uri.UriSchemeHttp),
				_ => false
			};
	}
}
