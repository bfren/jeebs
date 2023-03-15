// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Jeebs.WordPress.Functions;

public static partial class QueryPostsF
{
	/// <summary>
	/// Get Term Lists for specified model
	/// </summary>
	/// <typeparam name="TModel">Model type</typeparam>
	internal static List<PropertyInfo> GetTermLists<TModel>()
	{
		// Get term list properties for model type
		var taxonomies = from p in typeof(TModel).GetProperties()
						 where p.PropertyType == typeof(TermList)
						 select p;

		// If there aren't any return an empty list
		if (!taxonomies.Any())
		{
			return new();
		}

		return taxonomies.ToList();
	}
}
