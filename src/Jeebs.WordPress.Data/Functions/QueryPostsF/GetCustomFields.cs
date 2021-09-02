﻿// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Concurrent;
using System.Reflection;
using Jeebs.WordPress.Data;

namespace F.WordPressF.DataF
{
	public static partial class QueryPostsF
	{
		/// <summary>
		/// Custom Fields cache
		/// </summary>
		private static readonly ConcurrentDictionary<Type, IEnumerable<PropertyInfo>> customFieldsCache = new();

		/// <summary>
		/// Get Custom Fields for specified model
		/// </summary>
		/// <typeparam name="TModel">Model type</typeparam>
		internal static List<PropertyInfo> GetCustomFields<TModel>()
		{
			// Get from or Add to the cache
			var customFields = customFieldsCache.GetOrAdd(
				typeof(TModel),
				type => from cf in type.GetProperties()
						where typeof(ICustomField).IsAssignableFrom(cf.PropertyType)
						select cf
			);

			// If there aren't any return an empty list
			if (!customFields.Any())
			{
				return new();
			}

			return customFields.ToList();
		}
	}
}
