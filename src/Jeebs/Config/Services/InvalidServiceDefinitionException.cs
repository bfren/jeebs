// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Globalization;

namespace Jeebs.Config.Services;

/// <summary>
/// Invalid Service Definition
/// </summary>
public class InvalidServiceDefinitionException : Exception
{
	/// <summary>
	/// Exception message format
	/// </summary>
	public static readonly string Format = "Invalid service definition '{0}': should be [service_type].[service_name].";

	/// <summary>
	/// Create exception
	/// </summary>
	public InvalidServiceDefinitionException() { }

	/// <summary>
	/// Create exception
	/// </summary>
	/// <param name="definition"></param>
	public InvalidServiceDefinitionException(string definition) : base(string.Format(CultureInfo.InvariantCulture, Format, definition)) { }

	/// <summary>
	/// Create exception
	/// </summary>
	/// <param name="message"></param>
	/// <param name="inner"></param>
	public InvalidServiceDefinitionException(string message, Exception inner) : base(message, inner) { }
}
