// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Microsoft.Extensions.Options;

namespace Jeebs.Config.Services.Console;

/// <summary>
/// Console configuration.
/// </summary>
public sealed record class ConsoleConfig : IOptions<ConsoleConfig>, IServiceConfig
{
	/// <summary>
	/// Log message template.
	/// </summary>
	public string Template { get; init; } =
		"[{Timestamp:yyMMdd HHmm} {Level:u3}] {Message:lj} | {SourceContext}{NewLine}";

	/// <summary>
	/// Whether or not to add a prefix to the output.
	/// </summary>
	public bool AddPrefix { get; init; } =
		true;

	/// <inheritdoc/>
	public bool IsValid =>
		true;

	/// <inheritdoc/>
	ConsoleConfig IOptions<ConsoleConfig>.Value =>
		this;
}
