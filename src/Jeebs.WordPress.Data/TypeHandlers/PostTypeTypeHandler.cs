// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.uk/2013

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
