﻿// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Globalization;

namespace Jeebs.Config;

/// <summary>
/// Unknown / unsupported authentication scheme
/// </summary>
public class UnsupportedAuthenticationSchemeException : Exception
{
	/// <summary>
	/// Exception message format
	/// </summary>
	public static readonly string Format = "Unsupported authentication scheme '{0}'.";

	/// <summary>
	/// Create exception
	/// </summary>
	public UnsupportedAuthenticationSchemeException() { }

	/// <summary>
	/// Create exception
	/// </summary>
	/// <param name="scheme">Service type</param>
	public UnsupportedAuthenticationSchemeException(string scheme) : base(string.Format(CultureInfo.InvariantCulture, Format, scheme)) { }

	/// <summary>
	/// Create exception
	/// </summary>
	/// <param name="message"></param>
	/// <param name="inner"></param>
	public UnsupportedAuthenticationSchemeException(string message, Exception inner) : base(message, inner) { }
}
