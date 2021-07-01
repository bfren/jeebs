// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

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
		/// Post Content cache
		/// </summary>
		private static readonly ConcurrentDictionary<Type, IEnumerable<PropertyInfo>> contentCache = new();

		/// <summary>
		/// Get Post Content property for specified model
		/// </summary>
		/// <typeparam name="TModel">Model type</typeparam>
		internal static Option<PropertyInfo> GetPostContent<TModel>()
		{
			// Get from or Add to the cache
			var content = contentCache.GetOrAdd(
				typeof(TModel),
				type => from c in type.GetProperties()
						where c.Name == nameof(WpPostEntity.Content)
						&& c.PropertyType == typeof(string)
						select c
			);

			// If content is not defined return none
			if (!content.Any())
			{
				return None<PropertyInfo, Msg.ContentPropertyNotFoundMsg<TModel>>();
			}

			// Return single property
			// There cannot be more than one because the property is found by name, which must be unique
			return content.Single();
		}

		/// <summary>Messages</summary>
		public static partial class Msg
		{
			/// <summary>Content property not found on model</summary>
			/// <typeparam name="T">Post Model type</typeparam>
			public sealed record ContentPropertyNotFoundMsg<T> : IMsg { }
		}
	}
}
