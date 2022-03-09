// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;

namespace Jeebs.Config.Web.Redirections;

/// <summary>
/// Redirections configuration
/// </summary>
public sealed class RedirectionsConfig : Dictionary<string, string>
{
	/// <summary>
	/// Path to this configuration section
	/// </summary>
	public static readonly string Key = WebConfig.Key + ":redirections";
}
