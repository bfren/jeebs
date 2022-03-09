// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Messages;

/// <summary>
/// Framework 'Not Found' message with a value
/// </summary>
/// <typeparam name="T">Value type</typeparam>
public interface INotFoundMsg<T> : IWithValueMsg<T> { }
