// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Logging;

namespace Jeebs;

public static partial class ResultExtensions
{
	/// <inheritdoc cref="LogBool(Result{bool}, ILog, string, string)"/>
	public static Result<bool> LogBool(this Result<bool> @this, ILog usingLog) =>
		LogBool(@this, usingLog, "Done.", "Failed.");

	/// <summary>
	/// Log a boolean result - or the failure value if <see cref="Failure"/>.
	/// </summary>
	/// <param name="this">Result object.</param>
	/// <param name="usingLog">Log implementation.</param>
	/// <param name="ifTrue">Text when <paramref name="this"/> is <see cref="Ok{T}"/> and the value is true.</param>
	/// <param name="ifFalse">Text when <paramref name="this"/> is <see cref="Ok{T}"/> and the value is false.</param>
	/// <returns>Original object.</returns>
	public static Result<bool> LogBool(this Result<bool> @this, ILog usingLog, string ifTrue, string ifFalse) =>
		@this.Audit(
			ok: x =>
			{
				if (x)
				{
					usingLog.Inf(ifTrue);
				}
				else
				{
					usingLog.Err(ifFalse);
				}
			},
			fail: usingLog.Failure
		);
}
