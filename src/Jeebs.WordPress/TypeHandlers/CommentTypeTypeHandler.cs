// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.TypeHandlers;
using Jeebs.WordPress.Enums;

namespace Jeebs.WordPress.TypeHandlers;

/// <summary>
/// Comment TypeHandler.
/// </summary>
public sealed class CommentTypeTypeHandler : EnumeratedTypeHandler<CommentType>
{
	/// <summary>
	/// Parse the CommentType value.
	/// </summary>
	/// <param name="value">Database table value.</param>
	public override CommentType Parse(object value) =>
		Parse(value, CommentType.Parse, CommentType.Blank);
}
