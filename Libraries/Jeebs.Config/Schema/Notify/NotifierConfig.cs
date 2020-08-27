using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Config
{
	/// <summary>
	/// Notifier configuration
	/// </summary>
	public class NotifierConfig
	{
		/// <summary>
		/// Path to this configuration section
		/// </summary>
		public const string Key = JeebsConfig.Key + ":notifier";

		/// <summary>
		/// List of listeners - values should be Service names
		/// </summary>
		public List<string> Listeners { get; set; } = new List<string>();
	}
}
