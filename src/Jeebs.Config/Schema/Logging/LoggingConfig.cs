// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.uk/2013

using System.Collections.Generic;
using Jeebs.Config.Logging;
using Jeebs.Logging;

namespace Jeebs.Config
{
	/// <summary>
	/// Logging configuration
	/// </summary>
	public record LoggingConfig
	{
		/// <summary>
		/// Path to this configuration section
		/// </summary>
		public const string Key = JeebsConfig.Key + ":logging";

		/// <summary>
		/// Minimum LogLevel
		/// </summary>
		public LogLevel MinimumLevel { get; init; }

		/// <summary>
		/// If true, log to console
		/// </summary>
		public bool Console { get; init; }

		/// <summary>
		/// Set to override default output template for console messages
		/// </summary>
		public string ConsoleOutputTemplate { get; init; } = "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} | {SourceContext}{NewLine}{Exception}";

		/// <summary>
		/// If true the application name will be added before all console messages
		/// </summary>
		public bool AddPrefixToConsoleMessages { get; init; } = true;

		/// <summary>
		/// List of providers - dictionary key is a service name
		/// </summary>
		public Dictionary<string, LoggingProviderConfig> Providers { get; init; } = new();
	}
}
