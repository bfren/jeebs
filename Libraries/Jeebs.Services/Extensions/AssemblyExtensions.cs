using System;
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
			if (@this == null)
			{
				throw new ArgumentNullException(nameof(@this));
			}

			try
			{
				return @this.GetTypes();
			}
			catch (ReflectionTypeLoadException e)
			{
				return e.Types.Where(t => t != null);
			}
		}
	}
}
