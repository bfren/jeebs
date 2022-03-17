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
	internal static Lazy<List<AssemblyName>> AllAssemblyNames { get; } = new(
		() =>
		{
			var files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll");
			var names = new List<AssemblyName>();
			foreach (var file in files)
			{
				try
				{
					var name = AssemblyName.GetAssemblyName(file);
					var startsWith = bool (string ns) =>
						name.FullName.StartsWith(ns, StringComparison.InvariantCultureIgnoreCase);

					if (!startsWith("Microsoft.") && !startsWith("System."))
					{
						names.Add(name);
					}
				}
				catch
				{
					// Ignore errors - these assemblies cannot be loaded
				}
			}

			return names;
		},
		true
	);

	/// <summary>
	/// Return list of all public class types in all loaded assemblies -
	/// excludes Microsoft.* and System.* types
	/// </summary>
	internal static Lazy<IEnumerable<Type>> AllTypes { get; } = new(
		() =>
		{
			var types = new List<Type>();
			foreach (var name in AllAssemblyNames.Value)
			{
				try
				{
					var assembly = Assembly.Load(name);
					types.AddRange(assembly.GetTypes());
				}
				catch
				{
					// Ignore errors - these types cannot be loaded
				}
			}

			return types;
		},
		true
	);

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
		var types = from t in AllTypes.Value
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
		var types = from t in AllTypes.Value
					from p in t.GetProperties()
					where type.IsAssignableFrom(p.PropertyType)
					&& !p.PropertyType.IsAbstract
					&& !p.PropertyType.IsInterface
					&& !p.PropertyType.IsGenericParameter
					select p.PropertyType;

		return types.Distinct().ToList();
	}
}
