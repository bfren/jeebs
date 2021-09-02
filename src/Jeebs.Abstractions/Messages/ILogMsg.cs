// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Logging;

namespace Jeebs;

/// <summary>
/// Represents a framework message supporting logging
/// </summary>
public interface ILogMsg : IMsg
{
	/// <summary>
	/// Log level
	/// </summary>
	LogLevel Level { get; init; }

	/// <summary>
	/// Output format
	/// </summary>
	string Format { get; init; }

	/// <summary>
	/// Output arguments
	/// </summary>
	Func<object[]> Args { get; }
}
