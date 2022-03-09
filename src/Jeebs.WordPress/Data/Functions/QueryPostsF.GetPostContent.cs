// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Jeebs.Messages;
using Jeebs.WordPress.Data.Entities;
using Maybe;
using Maybe.Functions;

namespace Jeebs.WordPress.Data.Functions;

public static partial class QueryPostsF
{
	/// <summary>
	/// Post Content cache
	/// </summary>
	private static ConcurrentDictionary<Type, IEnumerable<PropertyInfo>> ContentCache { get; } = new();

	/// <summary>
	/// Get Post Content property for specified model
	/// </summary>
	/// <typeparam name="TModel">Model type</typeparam>
	internal static Maybe<PropertyInfo> GetPostContent<TModel>()
	{
		// Get from or Add to the cache
		var content = ContentCache.GetOrAdd(
			typeof(TModel),
			type => from c in type.GetProperties()
					where c.Name == nameof(WpPostEntity.Content)
					&& c.PropertyType == typeof(string)
					select c
		);

		// If content is not defined return none
		if (!content.Any())
		{
			return MaybeF.None<PropertyInfo, M.ContentPropertyNotFoundMsg<TModel>>();
		}

		// Return single property
		// There cannot be more than one because the property is found by name, which must be unique
		return content.Single();
	}

	/// <summary>Messages</summary>
	public static partial class M
	{
		/// <summary>Content property not found on model</summary>
		/// <typeparam name="T">Post Model type</typeparam>
		public sealed record class ContentPropertyNotFoundMsg<T> : Msg;
	}
}
