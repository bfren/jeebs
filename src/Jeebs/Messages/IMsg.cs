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
	/// Output format - placeholder values will be ignored and simply replaced in order with
	/// values from <see cref="Args"/>
	/// </summary>
	string Format { get; }

	/// <summary>
	/// Output format, including the message type which will be prepended to <see cref="Format"/>
	/// </summary>
	string FormatWithType { get; }

	/// <summary>
	/// Output arguments - will replace placeholders in <see cref="Format"/> in order
	/// </summary>
	object[]? Args { get; }

	/// <summary>
	/// Output arguments, including the message type which will be prepended to <see cref="Args"/>
	/// </summary>
	object[] ArgsWithType { get; }

	/// <summary>
	/// Return message type name
	/// </summary>
	string GetTypeName();
}
