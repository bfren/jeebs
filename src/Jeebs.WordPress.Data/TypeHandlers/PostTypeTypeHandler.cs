// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data.TypeHandlers;
using Jeebs.WordPress.Data.Enums;

namespace Jeebs.WordPress.Data.TypeHandlers
{
	/// <summary>
	/// PostType TypeHandler
	/// </summary>
	public sealed class PostTypeTypeHandler : EnumeratedTypeHandler<PostType>
	{
		/// <summary>
		/// Parse the PostType value
		/// </summary>
		/// <param name="value">Database table value</param>
		/// <returns>PostType object</returns>
		public override PostType Parse(object value) =>
			Parse(value, PostType.Parse, PostType.Post);
	}
}
