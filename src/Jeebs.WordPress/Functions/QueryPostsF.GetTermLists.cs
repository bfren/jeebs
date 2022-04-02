// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Jeebs.WordPress.Functions;

public static partial class QueryPostsF
{
	/// <summary>
	/// Taxonomies cache
	/// </summary>
	private static ConcurrentDictionary<Type, IEnumerable<PropertyInfo>> TermListsCache { get; } = new();

	/// <summary>
	/// Get Term Lists for specified model
	/// </summary>
	/// <typeparam name="TModel">Model type</typeparam>
	internal static List<PropertyInfo> GetTermLists<TModel>()
	{
		// Get from or Add to the cache
		var taxonomies = TermListsCache.GetOrAdd(
			typeof(TModel),
			type => from p in type.GetProperties()
					where p.PropertyType == typeof(TermList)
					select p
		);

		// If there aren't any return an empty list
		if (!taxonomies.Any())
		{
			return new();
		}

		return taxonomies.ToList();
	}
}
