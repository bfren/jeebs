// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Globalization;
using System.Text;

namespace Jeebs.Config.Services;

/// <summary>
/// Unsupported Service
/// </summary>
public class UnsupportedServiceException : Exception
{
	/// <summary>
	/// Exception message format
	/// </summary>
	public static readonly CompositeFormat Format = CompositeFormat.Parse("Unsupported service type '{0}'.");

	/// <summary>
	/// Create exception
	/// </summary>
	public UnsupportedServiceException() { }

	/// <summary>
	/// Create exception
	/// </summary>
	/// <param name="type">Service type</param>
	public UnsupportedServiceException(string type) : base(string.Format(CultureInfo.InvariantCulture, Format, type)) { }

	/// <summary>
	/// Create exception
	/// </summary>
	/// <param name="message"></param>
	/// <param name="inner"></param>
	public UnsupportedServiceException(string message, Exception inner) : base(message, inner) { }
}
