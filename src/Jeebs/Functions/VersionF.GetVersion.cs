// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.IO;
using System.Threading.Tasks;

namespace Jeebs.Functions;

public static partial class VersionF
{
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
	/// Return version formatted as x.x.x.x
	/// </summary>
	/// <param name="version">Version</param>
	internal static string GetVersionString(Version version) =>
		$"{version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
}
