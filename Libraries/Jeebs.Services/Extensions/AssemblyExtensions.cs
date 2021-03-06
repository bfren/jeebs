// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Jeebs.Services
{
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
}
