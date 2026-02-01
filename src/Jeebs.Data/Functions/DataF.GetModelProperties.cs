// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Jeebs.Data.Attributes;

namespace Jeebs.Data.Functions;

public static partial class DataF
{
	private const bool EnablePropertiesCache = true;

	/// <summary>
	/// Properties of models that have not been marked with <see cref="IgnoreAttribute"/>.
	/// </summary>
	private static ConcurrentDictionary<Type, IEnumerable<PropertyInfo>> PropertiesCache { get; } = new();

	/// <summary>
	/// Get all properties from a model not marked with <see cref="IgnoreAttribute"/>.
	/// </summary>
	/// <typeparam name="TModel">Model type.</typeparam>
	public static IEnumerable<PropertyInfo> GetModelProperties<TModel>()
	{
		return EnablePropertiesCache switch
		{
			true =>
				PropertiesCache.GetOrAdd(typeof(TModel), _ => get()),

			false =>
				get()
		};

		static IEnumerable<PropertyInfo> get() =>
			from p in typeof(TModel).GetProperties()
			where p.GetCustomAttribute<IgnoreAttribute>() is null
			select p;
	}
}
