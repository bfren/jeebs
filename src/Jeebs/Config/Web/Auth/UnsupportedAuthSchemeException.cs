// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Globalization;

namespace Jeebs.Config.Web.Auth;

/// <summary>
/// Unknown / unsupported authentication scheme
/// </summary>
public sealed class UnsupportedAuthSchemeException : Exception
{
	/// <summary>
	/// Exception message format
	/// </summary>
	public static readonly string Format = "Unsupported auth scheme '{0}'.";

	/// <summary>
	/// Create exception
	/// </summary>
	public UnsupportedAuthSchemeException() { }

	/// <summary>
	/// Create exception
	/// </summary>
	/// <param name="scheme">Service type</param>
	public UnsupportedAuthSchemeException(string scheme) : base(string.Format(CultureInfo.InvariantCulture, Format, scheme)) { }

	/// <summary>
	/// Create exception
	/// </summary>
	/// <param name="message"></param>
	/// <param name="inner"></param>
	public UnsupportedAuthSchemeException(string message, Exception inner) : base(message, inner) { }
}
