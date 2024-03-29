// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Jeebs.Services.Extensions;

internal static class AssemblyExtensions
{
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
