// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.IO;
using System.Threading.Tasks;

namespace Jeebs.Functions;

/// <summary>
/// Version functions
/// </summary>
public static class VersionF
{
	/// <summary>
	/// Lazy property to avoid multiple reflection calls
	/// </summary>
	private static LazyAsync<string> Version { get; } = new(
		() => GetVersion(typeof(VersionF).Assembly.GetManifestResourceStream("Jeebs.Version"), AssemblyVersion)
	);

	/// <summary>
	/// Get Jeebs Version, or return the package version if the resource cannot be found
	/// </summary>
	public static Task<string> GetJeebsVersionAsync() =>
		Version.Value;

	/// <summary>
	/// Get version from the specified stream, Version property, or default (0.0.0.0)
	/// </summary>
	/// <param name="stream">Stream containing version file</param>
	/// <param name="version">Version object</param>
	internal static async Task<string> GetVersion(Stream? stream, Version? version)
	{
		// Attempt to read from stream
		if (stream is Stream s)
		{
			try
			{
				using var reader = new StreamReader(s);
				return await reader.ReadToEndAsync().ConfigureAwait(false);
			}
			catch
			{
				// do nothing - the function will continue to try other methods
			}
		}

		// Attempt to read from version
		if (version is not null)
		{
			return GetVersionString(version);
		}

		// Retun an empty version (0.0.0.0)
		return GetVersionString(new(0, 0, 0, 0));
	}

	/// <summary>
	/// Get the compiled version of the current assembly - does not include suffixes (e.g. '-beta.1')
	/// </summary>
	internal static Version? AssemblyVersion =>
		typeof(VersionF).Assembly.GetName().Version;

	/// <summary>
	/// Return version formatted as x.x.x.x
	/// </summary>
	/// <param name="version">Version</param>
	internal static string GetVersionString(Version version) =>
		$"{version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
}
