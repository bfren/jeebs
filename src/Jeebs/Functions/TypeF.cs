// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Jeebs.Functions;

/// <summary>
/// Type functions
/// </summary>
public static class TypeF
{
	/// <summary>
	/// Return list of all loaded assembly names -
	/// excludes Microsoft.* and System.* assemblies
	/// See https://dotnetcoretutorials.com/2020/07/03/getting-assemblies-is-harder-than-you-think-in-c/
	/// </summary>
	internal static Lazy<List<AssemblyName>> Assemblies { get; } = new(
		() => Directory
			.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
			.Select(f => AssemblyName.GetAssemblyName(f))
			.Where(n =>
				!n.FullName.StartsWith("Microsoft.", StringComparison.InvariantCultureIgnoreCase) &&
				!n.FullName.StartsWith("System.", StringComparison.InvariantCultureIgnoreCase)
			)
			.ToList(),
		true
	);

	/// <summary>
	/// Return list of all public class types in all loaded assemblies -
	/// excludes Microsoft.* and System.* types
	/// </summary>
	internal static IEnumerable<Type> AllTypes =>
		from a in Assemblies.Value.Select(n => Assembly.Load(n))
		from t in a.GetTypes()
		where t.IsClass && t.IsPublic
		select t;

	/// <summary>
	/// Get distinct types that implement type <typeparamref name="T"/>
	/// </summary>
	/// <typeparam name="T">Base Type</typeparam>
	public static List<Type> GetTypesImplementing<T>() =>
		GetTypesImplementing(typeof(T));

	/// <summary>
	/// Get distinct types that implement <paramref name="type"/>
	/// </summary>
	/// <param name="type">Base Type</param>
	public static List<Type> GetTypesImplementing(Type type)
	{
		var types = from t in AllTypes
					where type.IsAssignableFrom(t)
					&& !t.IsAbstract
					&& !t.IsInterface
					select t;

		return types.Distinct().ToList();
	}

	/// <summary>
	/// Get distinct property types that implement type <typeparamref name="T"/>
	/// </summary>
	/// <typeparam name="T">Property Type</typeparam>
	public static List<Type> GetPropertyTypesImplementing<T>() =>
		GetPropertyTypesImplementing(typeof(T));

	/// <summary>
	/// Get distinct property types that implement <paramref name="type"/>
	/// </summary>
	/// <param name="type">Property Type</param>
	public static List<Type> GetPropertyTypesImplementing(Type type)
	{
		var types = from t in AllTypes
					from p in t.GetProperties()
					where type.IsAssignableFrom(p.PropertyType)
					&& !p.PropertyType.IsAbstract
					&& !p.PropertyType.IsInterface
					&& !p.PropertyType.IsGenericParameter
					select p.PropertyType;

		return types.Distinct().ToList();
	}
}
