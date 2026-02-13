// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Jeebs.Services;

internal static partial class AssemblyExtensions
{
	/// <summary>
	/// Load all types from an assembly.
	/// </summary>
	/// <param name="this">Assembly.</param>
	/// <returns>List of non-null types.</returns>
	internal static IEnumerable<Type> GetLoadableTypes(this Assembly @this)
	{
		try
		{
			return @this.GetTypes();
		}
		catch (ReflectionTypeLoadException e)
		{
			return from t in e.Types
				   where t != null
				   select t;
		}
	}
}
