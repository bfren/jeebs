// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.TypeHandlers;

/// <summary>
/// MimeType TypeHandler.
/// </summary>
public sealed class MimeTypeTypeHandler : EnumeratedTypeHandler<MimeType>
{
	/// <summary>
	/// Parse the MimeType value.
	/// </summary>
	/// <param name="value">Database table value.</param>
	public override MimeType Parse(object value) =>
		Parse(value, MimeType.Parse, MimeType.Blank);
}
