// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Data.TypeHandlers;
using Jeebs.WordPress.Data.Enums;

namespace Jeebs.WordPress.Data.TypeHandlers
{
	/// <summary>
	/// Comment TypeHandler
	/// </summary>
	public sealed class CommentTypeTypeHandler : EnumeratedTypeHandler<CommentType>
	{
		/// <summary>
		/// Parse the CommentType value
		/// </summary>
		/// <param name="value">Database table value</param>
		/// <returns>CommentType object</returns>
		public override CommentType Parse(object value) =>
			Parse(value, CommentType.Parse, CommentType.Blank);
	}
}
