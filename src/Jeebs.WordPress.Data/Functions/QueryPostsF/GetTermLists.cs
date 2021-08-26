// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Collections.Concurrent;
using System.Reflection;
using Jeebs.WordPress.Data;

namespace F.WordPressF.DataF
{
	public static partial class QueryPostsF
	{
		/// <summary>
		/// Taxonomies cache
		/// </summary>
		private static readonly ConcurrentDictionary<Type, IEnumerable<PropertyInfo>> termListsCache = new();

		/// <summary>
		/// Get Term Lists for specified model
		/// </summary>
		/// <typeparam name="TModel">Model type</typeparam>
		internal static List<PropertyInfo> GetTermLists<TModel>()
		{
			// Get from or Add to the cache
			var taxonomies = termListsCache.GetOrAdd(
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
}
