// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Jeebs.Data.Entities;

namespace F.DataF
{
	public static partial class QueryF
	{
		private const bool enablePropertiesCache = true;

		/// <summary>
		/// Properties of models that have not been marked with <see cref="IgnoreAttribute"/>
		/// </summary>
		private static readonly ConcurrentDictionary<Type, IEnumerable<PropertyInfo>> propertiesCache = new();

		/// <summary>
		/// Get all properties from a model not marked with <see cref="IgnoreAttribute"/>
		/// </summary>
		/// <typeparam name="TModel">Model type</typeparam>
		public static IEnumerable<PropertyInfo> GetModelProperties<TModel>()
		{
			return enablePropertiesCache switch
			{
				true =>
					propertiesCache.GetOrAdd(typeof(TModel), _ => get()),

				false =>
					get()
			};

			static IEnumerable<PropertyInfo> get() =>
				from p in typeof(TModel).GetProperties()
				where p.GetCustomAttribute<IgnoreAttribute>() == null
				select p;
		}
	}
}
