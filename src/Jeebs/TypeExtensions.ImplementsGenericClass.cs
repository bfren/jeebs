// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;

namespace Jeebs;

public static partial class TypeExtensions
{
	/// <summary>
	/// True if <paramref name="this"/> implements <paramref name="class"/>.
	/// </summary>
	/// <param name="this">Base type.</param>
	/// <param name="class">Class type.</param>
	/// <returns>Whether or not <paramref name="this"/> implements <paramref name="class"/>.</returns>
	internal static bool ImplementsGenericClass(this Type @this, Type @class) =>
		ImplementsGeneric(@this, @class) ||
		@this.BaseType switch
		{
			Type t =>
				ImplementsGenericClass(t, @class),

			_ =>
				false
		};
}
