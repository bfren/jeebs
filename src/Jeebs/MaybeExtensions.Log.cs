// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Logging;

namespace Jeebs;

public static partial class MaybeExtensions
{
	/// <inheritdoc cref="Log{T}(Maybe{T}, ILog, string)"/>
	public static Maybe<T> Log<T>(this Maybe<T> @this, ILog log) =>
		Log(@this, log, "Done: {Value}.");

	/// <summary>
	/// Log a result - or the reason if <see cref="None{T}"/>
	/// </summary>
	/// <typeparam name="T">Maybe type</typeparam>
	/// <param name="this"></param>
	/// <param name="log"></param>
	/// <param name="message">Log message - must contain one placeholder for the value.</param>
	public static Maybe<T> Log<T>(this Maybe<T> @this, ILog log, string message) =>
		@this.Audit(
			some: x => log.Inf(message, x ?? new object()),
			none: log.Msg
		);
}
