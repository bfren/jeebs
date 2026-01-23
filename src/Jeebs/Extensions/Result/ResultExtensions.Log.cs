// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Logging;

namespace Jeebs;

public static partial class ResultExtensions
{
	/// <inheritdoc cref="Log{T}(Result{T}, ILog, string)"/>
	public static Result<T> Log<T>(this Result<T> @this, ILog usingLog) =>
		Log(@this, usingLog, "Done: {Value}.");

	/// <summary>
	/// Log a result - or the reason if <see cref="Fail"/>.
	/// </summary>
	/// <typeparam name="T">Ok value type.</typeparam>
	/// <param name="this">Result object.</param>
	/// <param name="usingLog">Log implementation.</param>
	/// <param name="message">Log message - must contain one placeholder for the value.</param>
	/// <returns>Original object.</returns>
	public static Result<T> Log<T>(this Result<T> @this, ILog usingLog, string message) =>
		@this.Audit(
			ok: x => usingLog.Inf(message, x ?? new object()),
			fail: usingLog.Failure
		);
}
