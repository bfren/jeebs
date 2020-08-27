using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Jeebs.Config
{
	/// <summary>
	/// Third-party services configuration
	/// </summary>
	public class ServicesConfig
	{
		/// <summary>
		/// Seq configurations
		/// </summary>
		public Dictionary<string, SeqConfig> Seq { get; set; } = new Dictionary<string, SeqConfig>();

		/// <summary>
		/// Slack configurations
		/// </summary>
		public Dictionary<string, SlackConfig> Slack { get; set; } = new Dictionary<string, SlackConfig>();

		/// <summary>
		/// Twitter configurations
		/// </summary>
		public Dictionary<string, TwitterConfig> Twitter { get; set; } = new Dictionary<string, TwitterConfig>();
	}
}
