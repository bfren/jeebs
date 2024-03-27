// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Linq;

namespace Jeebs;

/// <summary>
/// Extension methods for <see cref="Type"/> objects.
/// </summary>
public static class TypeExtensions
{
	/// <summary>
	/// True if <paramref name="this"/> implements <typeparamref name="T"/>
	/// </summary>
	/// <typeparam name="T">Implementation type</typeparam>
	/// <param name="this">Base type</param>
	public static bool Implements<T>(this Type @this) =>
		@this.Implements(typeof(T));

	/// <summary>
	/// True if <paramref name="this"/> implements <paramref name="type"/>
	/// </summary>
	/// <param name="this">Base type</param>
	/// <param name="type">Implementation type</param>
	public static bool Implements(this Type @this, Type type)
	{
		// Handle base object
		if (@this == typeof(object))
		{
			return false;
		}

		// Handle identical types
		if (@this == type)
		{
			return false;
		}

		// Handle value types
		if (@this.IsValueType)
		{
			return false;
		}

		// Simple checks
		if (@this.IsSubclassOf(type) || type.IsAssignableFrom(@this))
		{
			return true;
		}

		// Handle generic types
		if (type.IsGenericType)
		{
			return type.IsInterface switch
			{
				true =>
					@this.ImplementsGenericInterface(type),

				false =>
					@this.ImplementsGenericClass(type)
			};
		}

		return false;
	}

	/// <summary>
	/// True if <paramref name="this"/> implements <paramref name="interface"/>
	/// </summary>
	/// <param name="this">Base type</param>
	/// <param name="interface">Interface type</param>
	internal static bool ImplementsGenericInterface(this Type @this, Type @interface) =>
		@this.GetInterfaces().Any(x => x.ImplementsGeneric(@interface));

	/// <summary>
	/// True if <paramref name="this"/> implements <paramref name="class"/>
	/// </summary>
	/// <param name="this">Base type</param>
	/// <param name="class">Class type</param>
	internal static bool ImplementsGenericClass(this Type @this, Type @class) =>
		ImplementsGeneric(@this, @class) ||
		@this.BaseType switch
		{
			Type t =>
				ImplementsGenericClass(t, @class),

			_ =>
				false
		};

	private static bool ImplementsGeneric(this Type @this, Type generic) =>
		@this.IsGenericType && (@this.GetGenericTypeDefinition() == generic || @this == generic);
}
