// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Jeebs;
using Jeebs.WordPress.Data.Entities;
using static F.OptionF;

namespace F.WordPressF.DataF
{
	public static partial class QueryPostsF
	{
		/// <summary>
		/// Get <see cref="PropertyInfo{TObject, TProperty}"/> object for <typeparamref name="TModel"/> content property
		/// </summary>
		/// <typeparam name="TModel">Model type</typeparam>
		internal static Option<Content<TModel>> GetPostContentInfo<TModel>()
		{
			return GetPostContent<TModel>().Map(x => new Content<TModel>(x), DefaultHandler);
		}

		internal class Content<TModel> : PropertyInfo<TModel, string>
		{
			internal Content(PropertyInfo info) : base(info) { }
		}
	}
}
