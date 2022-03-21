// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Functions.VersionF_Tests;

public static class Setup
{
	public static LazyAsync<string> JeebsVersion { get; } = new(
		async () =>
		{
			var stream = typeof(Setup).Assembly.GetManifestResourceStream("Jeebs.Version")!;
			using var reader = new StreamReader(stream);
			return await reader.ReadToEndAsync();
		}
	);

	public static Version NewVersion { get; } = new(
		Rnd.Int, Rnd.Int, Rnd.Int, Rnd.Int
	);
}
