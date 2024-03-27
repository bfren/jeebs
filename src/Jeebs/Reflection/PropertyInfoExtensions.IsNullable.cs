// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Reflection;

namespace Jeebs.Reflection;

public static partial class PropertyInfoExtensions
{
	/// <summary>
	/// Returns true if the property is a reference type or a nullable value type.
	/// </summary>
	/// <param name="this">PropertyInfo.</param>
	/// <returns>Whether or not <paramref name="this"/> is a reference type of a nullable value type.</returns>
	public static bool IsNullable(this PropertyInfo @this) =>
		!@this.PropertyType.IsValueType || F.IsNullableValueType(@this.PropertyType);
}
