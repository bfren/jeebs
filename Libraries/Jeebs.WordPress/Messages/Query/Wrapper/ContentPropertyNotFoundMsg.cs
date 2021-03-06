// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

using Jeebs.WordPress.Entities;

namespace Jeebs.WordPress.Messages.Query.Wrapper
{
	/// <summary>
	/// See <see cref="QueryWrapper.GetPostContent{TModel}"/>
	/// </summary>
	/// <typeparam name="TModel">Model type</typeparam>
	public sealed class ContentPropertyNotFoundMsg<TModel> : IMsg
	{
		/// <summary>
		/// Return message
		/// </summary>
		public override string ToString() =>
			$"{nameof(WpPostEntity.Content)} property not found (model: {typeof(TModel)}).";
	}
}
