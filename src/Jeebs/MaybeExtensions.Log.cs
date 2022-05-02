// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Logging;

namespace Jeebs;

public static partial class MaybeExtensions
{
	/// <inheritdoc cref="Log{T}(Maybe{T}, ILog, string)"/>
	public static Maybe<T> Log<T>(this Maybe<T> @this, ILog usingLog) =>
		Log(@this, usingLog, "Done: {Value}.");

	/// <summary>
	/// Log a result - or the reason if <see cref="None{T}"/>
	/// </summary>
	/// <typeparam name="T">Maybe type</typeparam>
	/// <param name="this"></param>
	/// <param name="usingLog"></param>
	/// <param name="message">Log message - must contain one placeholder for the value.</param>
	public static Maybe<T> Log<T>(this Maybe<T> @this, ILog usingLog, string message) =>
		@this.Audit(
			some: x => usingLog.Inf(message, x ?? new object()),
			none: usingLog.Msg
		);
}
