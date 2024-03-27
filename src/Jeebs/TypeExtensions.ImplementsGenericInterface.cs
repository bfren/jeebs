// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Linq;

namespace Jeebs;

public static partial class TypeExtensions
{
	/// <summary>
	/// True if <paramref name="this"/> implements <paramref name="interface"/>.
	/// </summary>
	/// <param name="this">Base type</param>
	/// <param name="interface">Interface type</param>
	/// <returns>Whether or not <paramref name="this"/> implements <paramref name="interface"/>.</returns>
	internal static bool ImplementsGenericInterface(this Type @this, Type @interface) =>
		@this.GetInterfaces().Any(x => x.ImplementsGeneric(@interface));
}
