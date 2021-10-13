// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;

namespace Jeebs;

/// <summary>
/// Represents a framework message for handling exceptions
/// </summary>
public interface IExceptionMsg : IMsg
{
	/// <summary>
	/// The exception that occurred
	/// </summary>
	Exception Exception { get; init; }
}
