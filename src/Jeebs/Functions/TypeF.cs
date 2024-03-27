// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Jeebs.Functions;

/// <summary>
/// Type functions.
/// </summary>
public static partial class TypeF
{
	/// <summary>
	/// Return list of all loaded assembly names - excludes Microsoft.* and System.* assemblies.
	/// </summary>
	/// <remarks>
	/// See https://dotnetcoretutorials.com/2020/07/03/getting-assemblies-is-harder-than-you-think-in-c/.
	/// </remarks>
	internal static Lazy<List<AssemblyName>> AllAssemblyNames { get; } =
		new(() =>
		{
			var files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll");
			var names = new List<AssemblyName>();
			foreach (var file in files)
			{
				try
				{
					var name = AssemblyName.GetAssemblyName(file);
					bool startsWith(string ns) =>
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
		}, true);

	/// <summary>
	/// Return list of all public class types in all loaded assemblies - excludes Microsoft.* and System.* types.
	/// </summary>
	public static Lazy<IEnumerable<Type>> AllTypes { get; } =
		new(() =>
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
		}, true);
}
