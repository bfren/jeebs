using System;

namespace Jeebs;

/// <summary>
/// Used during test so System Exceptions don't need to be thrown
/// </summary>
public class OptionTestException : Exception
{
	/// <summary>
	/// Create object
	/// </summary>
	public OptionTestException() { }

	/// <summary>
	/// Create object
	/// </summary>
	/// <param name="message">Message</param>
	public OptionTestException(string message) : base(message) { }

	/// <summary>
	/// Create object
	/// </summary>
	/// <param name="message">Message</param>
	/// <param name="inner">Inner Exception</param>
	public OptionTestException(string message, Exception inner) : base(message, inner) { }
}