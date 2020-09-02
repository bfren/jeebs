using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Jeebs.Services.Webhook
{
	public static class LogLevelExtensions
	{
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
