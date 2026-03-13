// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Globalization;
using System.Text;

namespace Jeebs.Config.Web.Auth;

/// <summary>
/// Unknown / unsupported authentication scheme.
/// </summary>
public sealed class UnsupportedAuthSchemeException : Exception
{
	/// <summary>
	/// Exception message format
	/// </summary>
	public static readonly CompositeFormat Format = CompositeFormat.Parse("Unsupported auth scheme '{0}'.");

	/// <inheritdoc/>
	public UnsupportedAuthSchemeException() { }

	/// <inheritdoc/>
	/// <param name="scheme">Service type (e.g. 'cookies').</param>
	public UnsupportedAuthSchemeException(string scheme) : base(string.Format(CultureInfo.InvariantCulture, Format, scheme)) { }

	/// <inheritdoc/>
	public UnsupportedAuthSchemeException(string message, Exception inner) : base(message, inner) { }
}
