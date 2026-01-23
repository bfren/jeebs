// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using System.Linq;

namespace Jeebs.Functions;

public static partial class TypeF
{
	/// <inheritdoc cref="GetPropertyTypesImplementing(Type, IEnumerable{Type})"/>
	/// <typeparam name="T">Property Type.</typeparam>
	public static List<Type> GetPropertyTypesImplementing<T>() =>
		GetPropertyTypesImplementing(typeof(T), AllTypes.Value);

	/// <inheritdoc cref="GetPropertyTypesImplementing(Type, IEnumerable{Type})"/>
	public static List<Type> GetPropertyTypesImplementing(Type type) =>
		GetPropertyTypesImplementing(type, AllTypes.Value);

	/// <summary>
	/// Get distinct property types that implement <paramref name="type"/>.
	/// </summary>
	/// <param name="type">Property Type.</param>
	/// <param name="typeList">Type List.</param>
	/// <returns>All types in <paramref name="typeList"/> that implement <paramref name="type"/>.</returns>
	internal static List<Type> GetPropertyTypesImplementing(Type type, IEnumerable<Type> typeList)
	{
		var types = from t in typeList
					from p in t.GetProperties()
					where type.IsAssignableFrom(p.PropertyType)
					&& !p.PropertyType.IsAbstract
					&& !p.PropertyType.IsInterface
					&& !p.PropertyType.IsGenericParameter
					select p.PropertyType;

		return types.Distinct().ToList();
	}
}
