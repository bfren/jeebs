// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Jeebs.WordPress.CustomFields;

namespace Jeebs.WordPress.Functions;

public static partial class QueryPostsF
{
	/// <summary>
	/// Get Custom Fields for specified model
	/// </summary>
	/// <typeparam name="TModel">Model type</typeparam>
	internal static List<PropertyInfo> GetCustomFields<TModel>()
	{
		// Get custom field properties for model type
		var customFields = from cf in typeof(TModel).GetProperties()
						   where typeof(ICustomField).IsAssignableFrom(cf.PropertyType)
						   select cf;

		// If there aren't any return an empty list
		if (!customFields.Any())
		{
			return new();
		}

		return customFields.ToList();
	}
}
