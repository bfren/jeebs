// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using System.Linq;

namespace Jeebs.Functions;

public static partial class TypeF
{
	/// <inheritdoc cref="GetPropertyTypesImplementingGeneric(Type, IEnumerable{Type})"/>
	public static List<Type> GetPropertyTypesImplementingGeneric(Type type) =>
		GetPropertyTypesImplementingGeneric(type, AllTypes.Value);

	/// <summary>
	/// Get distinct property types that implement <paramref name="type"/>.
	/// </summary>
	/// <param name="type">Property Type.</param>
	/// <param name="typeList">Type List.</param>
	internal static List<Type> GetPropertyTypesImplementingGeneric(Type type, IEnumerable<Type> typeList)
	{
		var types = from t in typeList
					from p in t.GetProperties()
					from i in p.PropertyType.GetInterfaces()
					where i.IsGenericType
					&& i.GetGenericTypeDefinition() == type
					&& !p.PropertyType.IsAbstract
					&& !p.PropertyType.IsInterface
					&& !p.PropertyType.IsGenericParameter
					select p.PropertyType;

		return types.Distinct().ToList();
	}
}
