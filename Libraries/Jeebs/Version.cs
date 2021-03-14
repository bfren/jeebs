// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Reflection;

namespace Jeebs
{
	/// <summary>
	/// Version
	/// </summary>
	/// <typeparam name="T">Class contained in assembly to get the version for</typeparam>
	public static class Version<T>
	{
		/// <summary>
		/// Get the version of the assembly the calling type belongs to
		/// </summary>
		public static Version V =>
			version.Value;

		/// <summary>
		/// Lazy property to avoid multiple reflection calls
		/// </summary>
		private static readonly Lazy<Version> version = new(
			() => typeof(T).GetTypeInfo().Assembly.GetName().Version ?? new Version()
		);

		/// <summary>
		/// Return a short version string (e.g. v1.1)
		/// </summary>
		public static string Short =>
			$"v{V.Major}.{V.Minor}";

		/// <summary>
		/// Return a build version string (e.g. v1.1.45)
		/// </summary>
		public static string Build =>
			$"v{V.Major}.{V.Minor}.{V.Build}";

		/// <summary>
		/// Return a full version string (e.g. v1.1.45.809)
		/// </summary>
		public static string Full =>
			$"v{V.Major}.{V.Minor}.{V.Build}.{V.Revision}";
	}
}
