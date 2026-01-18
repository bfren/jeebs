// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading.Tasks;
using Jeebs.Logging;

namespace Jeebs;

public static partial class ResultExtensions
{
	/// <inheritdoc cref="LogBool(Result{bool}, ILog, string, string)"/>
	public static async Task<Result<bool>> LogBoolAsync(this Task<Result<bool>> @this, ILog log) =>
		(await @this.ConfigureAwait(false)).LogBool(log);

	/// <inheritdoc cref="LogBool(Result{bool}, ILog, string, string)"/>
	public static async Task<Result<bool>> LogBoolAsync(this Task<Result<bool>> @this, ILog log, string ifTrue, string ifFalse) =>
		(await @this.ConfigureAwait(false)).LogBool(log, ifTrue, ifFalse);
}
