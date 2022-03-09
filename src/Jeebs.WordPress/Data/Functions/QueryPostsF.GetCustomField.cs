// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Reflection;
using Jeebs.WordPress.Data.CustomFields;

namespace Jeebs.WordPress.Data.Functions;

public static partial class QueryPostsF
{
	/// <summary>
	/// Get value of custom field - and if it's null, create it
	/// </summary>
	/// <typeparam name="TModel">Post model type</typeparam>
	/// <param name="post">Post object</param>
	/// <param name="info">Custom Field property info</param>
	internal static ICustomField? GetCustomField<TModel>(TModel post, PropertyInfo info)
	{
		if (info.GetValue(post) is ICustomField field)
		{
			return field;
		}

		return Activator.CreateInstance(info.PropertyType) switch
		{
			ICustomField customField =>
				customField,

			_ =>
				null
		};
	}
}
