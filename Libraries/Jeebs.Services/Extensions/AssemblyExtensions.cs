﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

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
