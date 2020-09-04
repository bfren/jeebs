using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Jeebs.Services.Webhook
{
	/// <summary>
	/// LogLevel Extensions - ToMessageLevel
	/// </summary>
	public static class LogLevelExtensions
	{
		/// <summary>
		/// Convert a <see cref="LogLevel"/> to a <see cref="MessageLevel"/>
		/// </summary>
		/// <param name="level"></param>
		public static MessageLevel ToMessageLevel(this LogLevel level)
			=> level switch
			{
				LogLevel.Trace => MessageLevel.Information,
				LogLevel.Debug => MessageLevel.Information,
				LogLevel.Warning => MessageLevel.Warning,
				LogLevel.Error => MessageLevel.Error,
				LogLevel.Critical => MessageLevel.Error,
				_ => MessageLevel.Information
			};
	}
}
