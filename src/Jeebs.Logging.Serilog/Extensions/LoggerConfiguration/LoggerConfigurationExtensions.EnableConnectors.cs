// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using Jeebs.Config;
using Jeebs.Functions;
using Serilog;

namespace Jeebs.Logging.Serilog;

public static partial class LoggerConfigurationExtensions
{
	/// <summary>
	/// Enable all <see cref="ILoggingConnector"/> services.
	/// </summary>
	/// <param name="serilog">Seilog configuration object.</param>
	/// <param name="jeebs">JeebsConfig.</param>
	internal static void EnableConnectors(LoggerConfiguration serilog, JeebsConfig jeebs)
	{
		// Get all connectors
		var connectors = new List<ILoggingConnector>();
		TypeF.GetTypesImplementing<ILoggingConnector>().ForEach(t =>
		{
			if (Activator.CreateInstance(t) is ILoggingConnector c)
			{
				connectors.Add(c);
			}
		});

		// Enable each connector
		foreach (var connector in connectors)
		{
			connector.Enable(serilog, jeebs);
		}
	}
}
