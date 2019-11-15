using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Config.Logging
{
	public sealed class LoggingProviders
	{
		/// <summary>
		/// Console Provider
		/// </summary>
		public ConsoleProvider Console { get; set; }

		/// <summary>
		/// File Provider
		/// </summary>
		public FileProvider File { get; set; }

		/// <summary>
		/// Slack Provider
		/// </summary>
		public SlackProvider Slack { get; set; }

		/// <summary>
		/// Setup object
		/// </summary>
		public LoggingProviders()
		{
			Console = new ConsoleProvider();
			File = new FileProvider();
			Slack = new SlackProvider();
		}
	}
}
