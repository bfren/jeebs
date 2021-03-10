// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Logging;

namespace Jeebs.Defaults
{
	/// <summary>
	/// Defaults for Logging
	/// </summary>
	public static class Log
	{
		/// <summary>
		/// Default log level is <see cref="LogLevel.Information"/>
		/// </summary>
		public const LogLevel Level = LogLevel.Information;

		/// <summary>
		/// Default exception log level is <see cref="LogLevel.Error"/>
		/// </summary>
		public const LogLevel ExceptionLevel = LogLevel.Error;
	}
}
