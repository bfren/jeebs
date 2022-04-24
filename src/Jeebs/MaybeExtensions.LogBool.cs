// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Logging;

namespace Jeebs;

public static partial class MaybeExtensions
{
	/// <inheritdoc cref="LogBool(Maybe{bool}, ILog, string, string)"/>
	public static Maybe<bool> LogBool(this Maybe<bool> @this, ILog log) =>
		LogBool(@this, log, "Done.", "Failed.");

	/// <summary>
	/// Log a boolean result - or the reason if <see cref="MaybeF.Internals.None{T}"/>
	/// </summary>
	/// <param name="this"></param>
	/// <param name="log"></param>
	/// <param name="done">Text on success</param>
	/// <param name="failed">Text on failure</param>
	public static Maybe<bool> LogBool(this Maybe<bool> @this, ILog log, string done, string failed) =>
		@this.Audit(
			some: x =>
			{
				if (x)
				{
					log.Inf(done);
				}
				else
				{
					log.Err(failed);
				}
			},
			none: log.Msg
		);
}
