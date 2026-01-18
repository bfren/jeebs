// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.TypeHandlers;
using Jeebs.WordPress.Enums;

namespace Jeebs.WordPress.TypeHandlers;

/// <summary>
/// PostStatus TypeHandler.
/// </summary>
public sealed class PostStatusTypeHandler : EnumeratedTypeHandler<PostStatus>
{
	/// <summary>
	/// Parse the PostStatus value.
	/// </summary>
	/// <param name="value">Database table value</param>
	public override PostStatus Parse(object value) =>
		Parse(value, PostStatus.Parse, PostStatus.Draft);
}
