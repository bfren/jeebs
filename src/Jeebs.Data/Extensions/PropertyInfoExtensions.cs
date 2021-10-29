// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Linq;
using System.Reflection;
using Jeebs.Data.Entities;

namespace Jeebs.Data;

public static class PropertyInfoExtensions
{
	public static bool DoNotWriteToDb(this PropertyInfo @this)
	{
		var attr = from a in @this.GetCustomAttributes()
				   where a is IdAttribute || a is IgnoreAttribute
				   select a;

		return attr.Any();
	}
}
