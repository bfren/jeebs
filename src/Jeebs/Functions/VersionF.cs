// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs;

namespace F;

/// <summary>
/// Version functions
/// </summary>
public static class VersionF
{
	/// <summary>
	/// Lazy property to avoid multiple reflection calls
	/// </summary>
	private static readonly LazyAsync<string> version = new(
		async () =>
		{
			// Attempt to get embedded Version file
			var versionResource = typeof(VersionF).Assembly.GetManifestResourceStream("Jeebs.Version");

			// If it doesn't exist, get the standard assembly / package version
			if (versionResource is null)
			{
				var v = typeof(VersionF).Assembly.GetName().Version ?? new();
				return $"v{v.Major}.{v.Minor}.{v.Build}.{v.Revision}";
			}

			// Read and return version
			using var reader = new StreamReader(versionResource);
			return await reader.ReadToEndAsync().ConfigureAwait(false);
		}
	);

	/// <summary>
	/// Get Jeebs Version, or return the package version if the resource cannot be found
	/// </summary>
	public static Task<string> GetJeebsVersionAsync() =>
		version.Value;
}
