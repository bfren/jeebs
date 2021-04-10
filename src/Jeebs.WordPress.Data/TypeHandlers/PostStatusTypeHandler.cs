// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

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
