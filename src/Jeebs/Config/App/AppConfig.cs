// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs.Functions;

namespace Jeebs.Config.App;

/// <summary>
/// Jeebs Application Configuraiton
/// </summary>
public sealed record class AppConfig
{
	/// <summary>
	/// Path to this configuration section
	/// </summary>
	public static readonly string Key = JeebsConfig.Key + ":app";

	/// <summary>
	/// Application Name
	/// </summary>
	public string Name { get; init; } = string.Empty;

	/// <summary>
	/// The Application Suite property to add to log messages
	/// </summary>
	public string? Suite { get; init; }

	/// <summary>
	/// The full name - if <see cref="Suite"/> is set, returns <see cref="Suite"/>/<see cref="Name"/>,
	/// otherwise simply <see cref="Name"/>
	/// </summary>
	public string FullName =>
		StringF.Format("{0}/", Suite, string.Empty) + Name;

	/// <summary>
	/// Application Version
	/// </summary>
	public Version Version { get; init; } = new Version(0, 1, 0, 0);
}
