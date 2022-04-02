// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Globalization;

namespace Jeebs.Config.Services;

/// <summary>
/// Invalid Service Configuration
/// </summary>
public class InvalidServiceConfigurationException : Exception
{
	/// <summary>
	/// Exception message format
	/// </summary>
	public static readonly string Format = "Service configuration '{0}' in {1} collection is not valid.";

	/// <summary>
	/// Create exception
	/// </summary>
	public InvalidServiceConfigurationException() { }

	/// <summary>
	/// Create exception
	/// </summary>
	/// <param name="message"></param>
	public InvalidServiceConfigurationException(string message) : base(message) { }

	/// <summary>
	/// Create exception
	/// </summary>
	/// <param name="message"></param>
	/// <param name="inner"></param>
	public InvalidServiceConfigurationException(string message, Exception inner) : base(message, inner) { }

	/// <summary>
	/// Create exception
	/// </summary>
	/// <param name="name"></param>
	/// <param name="type"></param>
	public InvalidServiceConfigurationException(string name, Type type) : this(string.Format(CultureInfo.InvariantCulture, Format, name, type)) { }
}
