// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;

namespace Jeebs.Functions;

public static partial class VersionF
{
	/// <summary>
	/// Lazy property to avoid multiple reflection calls.
	/// </summary>
	private static LazyAsync<string> Version { get; } =
		new(() => GetVersion(typeof(VersionF).Assembly.GetManifestResourceStream("Jeebs.Version"), AssemblyVersion));

	/// <summary>
	/// Get the compiled version of the current assembly - does not include suffixes (e.g. '-beta.1').
	/// </summary>
	internal static Version? AssemblyVersion =>
		typeof(VersionF).Assembly.GetName().Version;

	/// <summary>
	/// Get Jeebs Version, or return the package version if the resource cannot be found.
	/// </summary>
	public static Task<string> GetJeebsVersionAsync() =>
		Version.Value;
}
