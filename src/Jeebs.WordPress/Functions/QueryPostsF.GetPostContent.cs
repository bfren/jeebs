// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Linq;
using System.Reflection;
using Jeebs.WordPress.Entities;

namespace Jeebs.WordPress.Functions;

public static partial class QueryPostsF
{
	/// <summary>
	/// Get Post Content property for specified model.
	/// </summary>
	/// <typeparam name="TModel">Model type</typeparam>
	internal static Result<PropertyInfo> GetPostContent<TModel>()
	{
		// Get content property for model type
		var content = from c in typeof(TModel).GetProperties()
					  where c.Name == nameof(WpPostEntity.Content)
					  && c.PropertyType == typeof(string)
					  select c;

		// If content is not defined return none
		if (!content.Any())
		{
			return R.Fail("Content property not found on model '{Type}'.", typeof(TModel).Name)
				.Ctx(nameof(QueryPostsF), nameof(GetPostContent));
		}

		// Return single property
		// There cannot be more than one because the property is found by name, which must be unique
		return content.Single();
	}
}
