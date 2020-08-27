using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Config
{
	/// <summary>
	/// Redirections configuration
	/// </summary>
	public class RedirectionsConfig : Dictionary<string, string>
	{
		/// <summary>
		/// Path to this configuration section
		/// </summary>
		public const string Key = WebConfig.Key + ":redirections";
	}
}
