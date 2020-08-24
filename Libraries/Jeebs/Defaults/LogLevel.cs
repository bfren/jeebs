using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Jeebs.Defaults
{
	/// <summary>
	/// Defaults for Logging
	/// </summary>
	public static class Log
	{
		/// <summary>
		/// Default log level is <see cref="LogLevel.Debug"/>
		/// </summary>
		public const LogLevel Level = LogLevel.Debug;

		/// <summary>
		/// Default exception log level is <see cref="LogLevel.Error"/>
		/// </summary>
		public const LogLevel ExceptionLevel = LogLevel.Error;
	}
}
