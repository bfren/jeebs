// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;

namespace Jeebs;

/// <summary>
/// Extension methods for <see cref="Type"/> objects.
/// </summary>
public static partial class TypeExtensions
{
	private static bool ImplementsGeneric(this Type @this, Type generic) =>
		@this.IsGenericType && (@this.GetGenericTypeDefinition() == generic || @this == generic);
}
