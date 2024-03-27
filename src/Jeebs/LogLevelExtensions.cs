// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using MicrosoftLogLevel = Microsoft.Extensions.Logging.LogLevel;
using WrapLogLevel = Wrap.Logging.LogLevel;

namespace Jeebs;

/// <summary>
/// Extension methods for <see cref="WrapLogLevel"/> objects.
/// </summary>
public static class LogLevelExtensions
{
	/// <summary>
	/// Convert a <see cref="WrapLogLevel"/> to a <see cref="MicrosoftLogLevel"/>.
	/// </summary>
	/// <param name="level"><see cref="WrapLogLevel"/> value.</param>
	public static Maybe<MicrosoftLogLevel> ToMicrosoft(this WrapLogLevel level) =>
		level switch
		{
			WrapLogLevel.Verbose =>
				MicrosoftLogLevel.Trace,

			WrapLogLevel.Debug =>
				MicrosoftLogLevel.Debug,

			WrapLogLevel.Information =>
				MicrosoftLogLevel.Information,

			WrapLogLevel.Warning =>
				MicrosoftLogLevel.Warning,

			WrapLogLevel.Error =>
				MicrosoftLogLevel.Error,

			WrapLogLevel.Fatal =>
				MicrosoftLogLevel.Critical,

			_ =>
				M.None
		};
}
