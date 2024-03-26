// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using System.Linq;

namespace Jeebs.Functions;

public static partial class TypeF
{
	/// <inheritdoc cref="GetTypesImplementingGeneric(Type, IEnumerable{Type})"/>
	public static List<Type> GetTypesImplementingGeneric(Type type) =>
		GetTypesImplementingGeneric(type, AllTypes.Value);

	/// <summary>
	/// Get distinct types that implement a generic type <paramref name="type"/>.
	/// </summary>
	/// <param name="type">Base Type.</param>
	/// <param name="typeList">Type List.</param>
	internal static List<Type> GetTypesImplementingGeneric(Type type, IEnumerable<Type> typeList)
	{
		var types = from t in typeList
					from i in t.GetInterfaces()
					where i.IsGenericType
					&& i.GetGenericTypeDefinition() == type
					&& !t.IsAbstract
					&& !t.IsInterface
					select t;

		return types.Distinct().ToList();
	}
}
