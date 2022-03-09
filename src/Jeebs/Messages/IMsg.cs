// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Logging;
using Maybe;

namespace Jeebs.Messages;

/// <summary>
/// Framework message - compatible with <see cref="IReason"/>
/// </summary>
public interface IMsg : IReason
{
	/// <summary>
	/// Log Level of this message
	/// </summary>
	LogLevel Level { get; }

	/// <summary>
	/// Output format
	/// </summary>
	string Format { get; }

	/// <summary>
	/// Output format, including the message type
	/// </summary>
	string FormatWithType { get; }

	/// <summary>
	/// Output arguments
	/// </summary>
	object[]? Args { get; }

	/// <summary>
	/// Output arguments, including the message type
	/// </summary>
	object[] ArgsWithType { get; }

	/// <summary>
	/// Return message type name
	/// </summary>
	string GetTypeName();
}
