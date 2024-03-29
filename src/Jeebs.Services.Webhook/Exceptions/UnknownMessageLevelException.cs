﻿// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;

namespace Jx.Services.Webhook;

/// <summary>
/// Unknown MessageLevel
/// </summary>
public class UnknownMessageLevelException : Exception
{
	/// <summary>
	/// Create exception
	/// </summary>
	public UnknownMessageLevelException() { }

	/// <summary>
	/// Create exception
	/// </summary>
	/// <param name="message"></param>
	public UnknownMessageLevelException(string message) : base(message) { }

	/// <summary>
	/// Create exception
	/// </summary>
	/// <param name="message"></param>
	/// <param name="inner"></param>
	public UnknownMessageLevelException(string message, Exception inner) : base(message, inner) { }
}
