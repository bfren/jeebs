// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Config.Services.Console;

/// <summary>
/// Console configuration
/// </summary>
public sealed record class ConsoleConfig : IServiceConfig
{
	/// <summary>
	/// Override default template
	/// </summary>
	public string? Template { get; init; }

	/// <summary>
	/// Whether or not to add a prefix to the output
	/// </summary>
	public bool AddPrefix { get; init; } = true;

	/// <inheritdoc/>
	public bool IsValid =>
		true;
}
