// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Linq;
using System.Reflection;

namespace Jeebs.Reflection;

/// <summary>
/// Extension methods for <see cref="PropertyInfo"/> objects.
/// </summary>
public static class PropertyInfoExtensions
{
	/// <summary>
	/// Returns true if the property has been marked as nullable using the C# 8+ nullable feature.
	/// </summary>
	/// <param name="this">PropertyInfo.</param>
	public static bool IsNullable(this PropertyInfo @this) =>
		(@this.PropertyType.GenericTypeArguments.Length == 1 && @this.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
		|| @this.CustomAttributes.Any(a => a.AttributeType.FullName == "System.Runtime.CompilerServices.NullableAttribute");
}
