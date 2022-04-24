// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading.Tasks;
using Jeebs.Logging;

namespace Jeebs;

public static partial class MaybeExtensions
{
	/// <inheritdoc cref="LogBool(Maybe{bool}, ILog, string, string)"/>
	public static Task<Maybe<bool>> LogBoolAsync(this Task<Maybe<bool>> @this, ILog log) =>
		LogBoolAsync(@this, log, "Done.", "Failed.");

	/// <inheritdoc cref="LogBool(Maybe{bool}, ILog, string, string)"/>
	public static async Task<Maybe<bool>> LogBoolAsync(this Task<Maybe<bool>> @this, ILog log, string done, string failed) =>
		(await @this.ConfigureAwait(false)).LogBool(log, done, failed);
}
