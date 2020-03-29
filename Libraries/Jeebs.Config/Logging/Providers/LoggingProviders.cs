using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Config.Logging
{
	/// <summary>
	/// Logging Providers
	/// </summary>
	public sealed class LoggingProviders
	{
		/// <summary>
		/// Console Provider
		/// </summary>
		public ConsoleProvider Console { get; set; } = new ConsoleProvider();

		/// <summary>
		/// File Provider
		/// </summary>
		public FileProvider File { get; set; } = new FileProvider();

		/// <summary>
		/// Slack Provider
		/// </summary>
		public SlackProvider Slack { get; set; } = new SlackProvider();
	}
}
