// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Reflection;
using Jeebs.Reflection;

namespace Jeebs.WordPress.Functions;

public static partial class QueryPostsF
{
	/// <summary>
	/// Get <see cref="PropertyInfo{TObject, TProperty}"/> object for <typeparamref name="TModel"/> content property.
	/// </summary>
	/// <typeparam name="TModel">Model type</typeparam>
	internal static Result<Content<TModel>> GetPostContentInfo<TModel>() =>
		GetPostContent<TModel>().Map(x => new Content<TModel>(x));

	internal sealed class Content<TModel> : PropertyInfo<TModel, string>
	{
		internal Content(PropertyInfo info) : base(info) { }
	}
}
