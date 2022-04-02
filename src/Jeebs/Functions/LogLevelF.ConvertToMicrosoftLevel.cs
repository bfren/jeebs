// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Messages;
using JeebsLevel = Jeebs.Logging.LogLevel;
using MicrosoftLevel = Microsoft.Extensions.Logging.LogLevel;

namespace Jeebs.Functions;

public static partial class LogLevelF
{
	/// <summary>
	/// Convert a <see cref="JeebsLevel"/> to a <see cref="MicrosoftLevel"/>
	/// </summary>
	/// <param name="level"></param>
	public static Maybe<MicrosoftLevel> ConvertToMicrosoftLevel(JeebsLevel level) =>
		level switch
		{
			JeebsLevel.Verbose =>
				MicrosoftLevel.Trace,

			JeebsLevel.Debug =>
				MicrosoftLevel.Debug,

			JeebsLevel.Information =>
				MicrosoftLevel.Information,

			JeebsLevel.Warning =>
				MicrosoftLevel.Warning,

			JeebsLevel.Error =>
				MicrosoftLevel.Error,

			JeebsLevel.Fatal =>
				MicrosoftLevel.Critical,

			_ =>
				F.None<MicrosoftLevel>(new M.UnknownLogLevelMsg(level))
		};

	public static partial class M
	{
		/// <summary>Unknown LogLevel value</summary>
		/// <param name="Value"></param>
		public sealed record class UnknownLogLevelMsg(JeebsLevel Value) : WithValueMsg<JeebsLevel>;
	}
}
