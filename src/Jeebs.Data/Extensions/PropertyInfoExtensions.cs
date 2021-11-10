// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Linq;
using System.Reflection;
using Jeebs.Data.Entities;

namespace Jeebs.Data;

/// <summary>
/// PropertyInfo extensions: IsReadonly
/// </summary>
public static class PropertyInfoExtensions
{
	/// <summary>
	/// Returns true if the specified PropertyInfo has either <see cref="IdAttribute"/>, <see cref="IgnoreAttribute"/>,
	/// <see cref="ComputedAttribute"/>, or <see cref="ReadonlyAttribute"/>
	/// </summary>
	/// <param name="this">PropertyInfo</param>
	public static bool IsReadonly(this PropertyInfo @this)
	{
		if (!@this.CustomAttributes.Any())
		{
			return false;
		}

		var attr = from a in @this.GetCustomAttributes()
				   where a is IdAttribute || a is IgnoreAttribute || a is ComputedAttribute || a is ReadonlyAttribute
				   select a;

		return attr.Any();
	}
}
