// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Messages;

/// <summary>
/// Framework message with a value
/// </summary>
/// <typeparam name="T">Value type</typeparam>
public interface IWithValueMsg<T> : IMsg
{
	/// <summary>
	/// Message Value property name
	/// </summary>
	string Name { get; }

	/// <summary>
	/// Message Value
	/// </summary>
	T Value { get; init; }
}
