// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using System.Linq;

namespace Jeebs.Functions;

public static partial class TypeF
{
	/// <inheritdoc cref="GetTypesImplementing(Type, IEnumerable{Type})"/>
	/// <typeparam name="T">Base Type</typeparam>
	public static List<Type> GetTypesImplementing<T>() =>
		GetTypesImplementing(typeof(T), AllTypes.Value);

	/// <inheritdoc cref="GetTypesImplementing(Type, IEnumerable{Type})"/>
	public static List<Type> GetTypesImplementing(Type type) =>
		GetTypesImplementing(type, AllTypes.Value);

	/// <summary>
	/// Get distinct types that implement <paramref name="type"/>
	/// </summary>
	/// <param name="type">Base Type</param>
	/// <param name="typeList">Type List</param>
	internal static List<Type> GetTypesImplementing(Type type, IEnumerable<Type> typeList)
	{
		var types = from t in typeList
					where type.IsAssignableFrom(t)
					&& !t.IsAbstract
					&& !t.IsInterface
					select t;

		return types.Distinct().ToList();
	}
}
