// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Linq;

namespace Jeebs.Reflection;

public static partial class ObjectExtensions
{
	/// <summary>
	/// Return whether or not the object contains the specified property.
	/// </summary>
	/// <param name="this">Object.</param>
	/// <param name="propertyName">The name of the property whose value you want to return.</param>
	/// <returns>Whether or not <paramref name="this"/> contains a property named <paramref name="propertyName"/>.</returns>
	public static bool HasProperty(this object @this, string propertyName) =>
		GetProperties(@this).Any(x => x.Name == propertyName);
}
