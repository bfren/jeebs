// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Concurrent;
using System.Reflection;
using Jeebs;
using Jeebs.WordPress.Data;
using static F.OptionF;

namespace F.WordPressF.DataF;

public static partial class QueryPostsF
{
	/// <summary>
	/// Meta Dictionary cache
	/// </summary>
	private static readonly ConcurrentDictionary<Type, IEnumerable<PropertyInfo>> metaDictionaryCache = new();

	/// <summary>
	/// Get the Meta Dictionary Info for <typeparamref name="TModel"/>
	/// </summary>
	/// <typeparam name="TModel">Post Model type</typeparam>
	internal static Option<Meta<TModel>> GetMetaDictionary<TModel>()
	{
		// Get from or Add to the cache
		var metaDictionary = metaDictionaryCache.GetOrAdd(
			typeof(TModel),
			type => from m in type.GetProperties()
					where m.PropertyType == typeof(MetaDictionary)
					select m
		);

		// If MetaDictionary is not defined return none
		if (!metaDictionary.Any())
		{
			return None<Meta<TModel>, Msg.MetaDictionaryPropertyNotFoundMsg<TModel>>();
		}

		// Throw an error if there are multiple MetaDictionaries
		if (metaDictionary.Count() > 1)
		{
			return None<Meta<TModel>, Msg.MoreThanOneMetaDictionaryMsg<TModel>>();
		}

		return new Meta<TModel>(metaDictionary.Single());
	}

	/// <summary>
	/// Meta Info alias
	/// </summary>
	/// <typeparam name="TModel">Post Model type</typeparam>
	public sealed class Meta<TModel> : PropertyInfo<TModel, MetaDictionary>
	{
		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="info">PropertyInfo</param>
		public Meta(PropertyInfo info) : base(info) { }
	}

	public static partial class Msg
	{
		/// <summary>MetaDictionary property not found on <typeparamref name="T"/></summary>
		/// <typeparam name="T">Post type</typeparam>
		public sealed record class MetaDictionaryPropertyNotFoundMsg<T> : IMsg { }

		/// <summary>Multiple MetaDictionary properties found on <typeparamref name="T"/></summary>
		/// <typeparam name="T">Post type</typeparam>
		public sealed record class MoreThanOneMetaDictionaryMsg<T> : IMsg { }
	}
}
