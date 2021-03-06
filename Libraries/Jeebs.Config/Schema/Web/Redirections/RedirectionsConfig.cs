// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

using System.Collections.Generic;

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
