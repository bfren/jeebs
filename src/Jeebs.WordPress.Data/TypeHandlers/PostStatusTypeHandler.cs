// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Data.TypeHandlers;
using Jeebs.WordPress.Data.Enums;

namespace Jeebs.WordPress.Data.TypeHandlers
{
	/// <summary>
	/// PostStatus TypeHandler
	/// </summary>
	public sealed class PostStatusTypeHandler : EnumeratedTypeHandler<PostStatus>
	{
		/// <summary>
		/// Parse the PostStatus value
		/// </summary>
		/// <param name="value">Database table value</param>
		/// <returns>PostStatus object</returns>
		public override PostStatus Parse(object value) =>
			Parse(value, PostStatus.Parse, PostStatus.Draft);
	}
}
