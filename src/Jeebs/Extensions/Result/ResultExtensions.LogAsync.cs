// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading.Tasks;
using Jeebs.Logging;

namespace Jeebs;

public static partial class ResultExtensions
{
	/// <inheritdoc cref="Log{T}(Result{T}, ILog, string)"/>
	public static async Task<Result<T>> LogAsync<T>(this Task<Result<T>> @this, ILog log) =>
		(await @this).Log(log, "Done: {Value}.");

	/// <inheritdoc cref="Log{T}(Result{T}, ILog, string)"/>
	public static async Task<Result<T>> LogAsync<T>(this Task<Result<T>> @this, ILog log, string message) =>
		(await @this).Log(log, message);
}
