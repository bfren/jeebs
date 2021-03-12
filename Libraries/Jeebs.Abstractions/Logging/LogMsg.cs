// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Logging;

namespace Jeebs
{
	/// <inheritdoc cref="ILogMsg"/>
	public abstract record LogMsg(LogLevel Level) : ILogMsg
	{
		/// <summary>Use default Log Level (<see cref="LogLevel.Information"/>)</summary>
		public LogMsg() : this(LogLevel.Information) { }
	}
}
